using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing;

namespace MabelCardPrinter
{

    public class MabelResponse
    {
        public int code;
        public string message;
        public bool isError;
        public JToken results;
    }
    /// <summary>
    /// Represents a card as generated in Mabel
    /// </summary>
    public class MabelCard
    {
        public int card_id;
        public int member_id;
        public string mag_token;
        public string rfid_token;
        public string card_front_image_encoded; // base64 encoded
        /// <summary>
        /// Gets the front image of the card
        /// </summary>
        /// <returns>Image object</returns>
        public Image GetCardFrontImage()
        {
            if (card_front_image_encoded == null)
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(card_front_image_encoded);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
        public string card_back_image_encoded; // base64 encoded
        public Image GetCardBackImage()
        {
            if (card_back_image_encoded == null)
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(card_back_image_encoded);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

    }

    public class MabelAPI
    {
        private static String _baseAddress = "http://cardhandler.jamsandbox.com/print.php";

        private MabelResponse MakeRequest (String method, String parameters)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            //string jsonParameters = JsonConvert.SerializeObject(parameters);

            WebRequest request = HttpWebRequest.Create(_baseAddress + "?apiKey=123&modFunc=printer." + method + "&" + parameters);
            request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            //request.Credentials = new NetworkCredential(Properties.Settings.Default.Username, Properties.Settings.Default.Password, Properties.Settings.Default.Domain);
            //request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            //Console.WriteLine(request.ToString);
            // Get the response.
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
            Console.WriteLine(responseFromServer);
            JObject o = JObject.Parse(responseFromServer);
            MabelResponse mabelResponse =  new MabelResponse();
            mabelResponse.code = (int)o["meta"]["status"];
            mabelResponse.message = (string)o["meta"]["msg"];
            if (mabelResponse.code != 200)
            {
                // it's an error, return it now, don't bother looking for results
                mabelResponse.isError = true;
                return mabelResponse;
            }
            // otherwise, hand back the results :)
            mabelResponse.isError = false;
            // JToken result = o["result"];
            if (o["result"].Type != JTokenType.Null)
            {
                mabelResponse.results = o["result"];
            } else
            {
                mabelResponse.results = null;
            }
            return mabelResponse;
        }
        /// <summary>
        /// Registers the printer
        /// </summary>
        /// <param name="printerId">The printer ID to register</param>
        /// <param name="printerName">The "name" of the printer</param>
        /// <param name="printerLocation">The location of the printer</param>
        /// <param name="printerModel">The model of the printer</param>
        /// <returns></returns>
        public MabelResponse RegisterPrinter(int printerId, string printerName, string printerLocation, string printerModel)
        {
            MabelResponse response = MakeRequest("printerRegister", "printer_id=" + printerId + "&name=" + printerName + "&location=" + printerLocation + "&model=" + printerModel);
            if (response.isError)
            {
                Console.WriteLine("Argh, an error occurred!!");
            }
            return response;
        }

        /// <summary>
        /// Unregisters a printer
        /// </summary>
        /// <param name="printerId">The printer's ID</param>
        /// <returns></returns>
        public MabelResponse UnregisterPrinter(int printerId)
        {
            MabelResponse response = MakeRequest("printerUnregister", "printer_id=" + printerId);
            if (response.isError)
            {
                Console.WriteLine("Argh, an error occurred!!");
            }
            return response;
        }

        public MabelResponse SetPrinterStatus(int printerId, string status)
        {
            MabelResponse response = MakeRequest("printerSetStatus", "printer_id=" + printerId + "&status=" + status);
            if (response.isError)
            {
                Console.WriteLine("Argh, an error occurred!!");
            }
            return response;
        }

        public MabelResponse SetCardStatus(int printerId, MabelCard card, string status)
        {
            MabelResponse response = MakeRequest("cardSetStatus", "printer_id=" + printerId + "&card_id=" + card.card_id + "&status=" + status);
            if (response.isError)
            {
                Console.WriteLine("Argh, an error occurred!!");
            }
            return response;
        }

        public MabelResponse SetCardRfid(int printerId, MabelCard card, string rfid)
        {
            MabelResponse response = MakeRequest("cardSetStatus", "printer_id=" + printerId + "&card_id=" + card.card_id + "&rfid_token=" + rfid);
            if (response.isError)
            {
                Console.WriteLine("Argh, an error occurred!!");
            }
            return response;
        }

        public MabelResponse SetCardPrinted(int printerId, MabelCard card)
        {
            MabelResponse response = MakeRequest("cardPrinted", "printer_id=" + printerId + "&card_id=" + card.card_id);
            if (response.isError)
            {
                Console.WriteLine("Argh, an error occurred!!");
            }
            return response;
        }

        public MabelCard GetNextJob(int printerId)
        {
            MabelResponse response = MakeRequest("printerGetNextJob", "printer_id=" + printerId );
            if (response.results == null)
            {
                if (response.code == 200)
                {
                    // actually, just no cards to print :)
                    return null;
                }
            }
            MabelCard card = new MabelCard();
            card.card_id = (int) response.results.SelectToken("card_id");
            card.member_id = (int) response.results.SelectToken("member_id");
            card.mag_token = (string)response.results.SelectToken("card_mag_token");
            card.rfid_token = (string)response.results.SelectToken("card_rfid_token");
            card.card_front_image_encoded = (string)response.results.SelectToken("card_image_front");
            card.card_back_image_encoded = (string)response.results.SelectToken("card_image_back");
            return card;
        }

    }
}
