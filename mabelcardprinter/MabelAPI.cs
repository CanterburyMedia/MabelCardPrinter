using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing;
using System.Web;

namespace MabelCardPrinter
{


    public delegate void MabelEventHandler(object sender, MabelEventArgs e);

    public class MabelEventArgs : EventArgs
    {
        public String URL;
        public MabelResponse response;
        public MabelRequest request;
        public MabelEventArgs(String URL, MabelRequest request, MabelResponse response)
        {
            this.URL = URL;
            this.request = request;
            this.response = response;
        }
    }

    public class MabelResponse
    {
        public int code;
        public string message;
        public bool isError;
        public JToken results;
        public String _raw;
    }

    public class MabelRequest
    {
        private String _baseAddress;
        public String apiKey;
        public String modFunc;
        public MabelRequestParams param;
        public MabelRequest(MabelAPI api,String modFunc, MabelRequestParams param)
        {
            this.apiKey = Properties.Settings.Default.APIKey;
            this._baseAddress = api.getBaseUrl();
            this.modFunc = modFunc;
            this.param = param;
        }

        public String buildURL()
        {
            string jsonParameters = JsonConvert.SerializeObject(this);
            //jsonParameters = WebUtility.UrlEncode(jsonParameters);
            return _baseAddress + "?please=" + jsonParameters;
        }
    }
    public abstract class MabelRequestParams { };

    public class MabelPrinterRegisterParams : MabelRequestParams
    {
        public int printerId;
        public String name;
        public String location;
        public PrinterInfo info;
        public MabelPrinterRegisterParams(int printerId,String name, String location, PrinterInfo info)
        {
            this.printerId = printerId;
            this.name = name;
            this.location = location;
            this.info = info;
        }
    }

    public class MabelPrinterUnregisterParams : MabelRequestParams
    {
        public int printerId;

        public MabelPrinterUnregisterParams(int printerId)
        {
            this.printerId = printerId;
        }
    }

    public class MabelPrinterCheckVersionParams : MabelRequestParams
    {
        public int printerId;
        public MabelPrinterCheckVersionParams(int printerId)
        {
            this.printerId = printerId;
        }
    }

    public class MabelPrinterSetStatus : MabelRequestParams
    {
        public int printerId;
        public String status;
        public MabelPrinterSetStatus(int printerId,String status)
        {
            this.printerId = printerId;
            this.status = status;
        }

    }

    public class MabelSetCardStatusParams : MabelRequestParams {
        public int cardId;
        public String status;
        public MabelSetCardStatusParams(int cardId, String status)
        {
            this.cardId = cardId;
            this.status = status;
        }
    }

    public class MabelSetCardRfidParams : MabelRequestParams
    {
        public int printerId;
        public int cardId;
        public String rfid;
        public MabelSetCardRfidParams(int printerId, int cardId, String rfid)
        {
            this.printerId = printerId;
            this.cardId = cardId;
            this.rfid = rfid;
        }
    }

    public class MabelSetCardPrinterParams : MabelRequestParams
    {
        public int cardId;

        public MabelSetCardPrinterParams( int cardId)
        {
            this.cardId = cardId;
        }
    }

    public class MabelGetNextJobParams : MabelRequestParams
    {
        public int printerId;
        public MabelGetNextJobParams(int printerId)
        {
            this.printerId = printerId;
        }
    }

    public class MabelSetTokenParams : MabelRequestParams
    {
        public String token;
        public int printerId;
        public MabelSetTokenParams(String token,  int printerId)
        {
            this.token = token;
            this.printerId = printerId;
        }
    }
        
    /// <summary>
    /// Represents a card as generated in Mabel
    /// </summary>
    public class MabelCard
    {
        public int card_id;
        public String member_id;
        public string mag_token;
        public string rfid_token;
        public string card_front_image_encoded; // base64 encoded
        public delegate void PrinterEventHander(object sender, PrinterEventArgs e);


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
        public String _baseUrl = "";
        public event MabelEventHandler Debug;
        public MabelRequest lastRequest;
        public MabelResponse lastResponse;
        
        /// <summary>
        /// Gets the current base URL for this instance of the Mabel API. Defaults to the value in "settings" 
        /// if base url isn't specified - so if you have updated that Setting and want start using the new URL, you 
        /// need to create a new instance of Mabel API or use the setBaseUrl method to set the new base point.
        /// </summary>
        /// <returns></returns>
        public String getBaseUrl()
        {
            if (_baseUrl.Equals(""))
            {
                return Properties.Settings.Default.apiBaseUrl;
            } else
            {
                return this._baseUrl;
            }
        }

        /// <summary>
        /// Sets the base URL to use for this instance of the Mabel API (e.g. for testing connections)
        /// </summary>
        /// <param name="baseUrl"></param>
        public void setBaseUrl(String baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        // event handlers
        protected virtual void OnDebug(MabelEventArgs e)
        {
            Debug?.Invoke(this, e);
        }

        /// <summary>
        /// Makess a request to the Mabel API, using the specified request object
        /// </summary>
        /// <param name="mabelRequest">A build MabelRequest object with the desired params for the function call.</param>
        /// <returns></returns>
        private MabelResponse MakeRequest ( MabelRequest mabelRequest)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;
            String url = mabelRequest.buildURL();
            WebRequest request = WebRequest.Create(url);
            //request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            //request.Credentials = new NetworkCredential(Properties.Settings.Default.Username, Properties.Settings.Default.Password, Properties.Settings.Default.Domain);
            //request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            // Get the response.
            lastRequest = mabelRequest;
            MabelResponse mabelResponse = new MabelResponse();
            HttpWebResponse response = null;
            try
            {
                 response = (HttpWebResponse)request.GetResponse();
                lastResponse = mabelResponse;
            }
            catch (Exception ex)
            {
                mabelResponse.isError = true;
                mabelResponse.message = "HTTP EXCEPTION:" + ex.Message;
            } 
            if (response != null)
            { 
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
                mabelResponse._raw = responseFromServer;
            }
            
            try { 
                // Get the stream containing content returned by the server.
                JObject o = JObject.Parse(mabelResponse._raw);

                mabelResponse.code = (int)o["meta"]["status"];
                mabelResponse.message = (string)o["meta"]["msg"];
                if (mabelResponse.code != 200)
                {
                    // it's an error, return it now, don't bother looking for results
                    mabelResponse.isError = true;
                } else
                {
                    mabelResponse.isError = false;
                }
                // otherwise, hand back the results :)
               
                // JToken result = o["result"];
                if (o["result"].Type != JTokenType.Null)
                {
                    mabelResponse.results = o["result"];
                }
                else
                {
                    mabelResponse.results = null;
                }
            } catch (Exception ex)
            {
                mabelResponse.isError = true;
                mabelResponse.message = "JSON Exception: " + ex.Message;
            }
            
            OnDebug(new MabelEventArgs(url, mabelRequest, mabelResponse));
            
            return mabelResponse;
        }

        // API Functions  //

        /// <summary>
        /// Registers the printer
        /// </summary>
        /// <param name="printerId">The printer ID to register</param>
        /// <param name="printerName">The "name" of the printer (as in windows)</param>
        /// <param name="printerLocation">The location of the printer</param>
        /// <param name="printerModel">The model of the printer</param>
        /// <returns></returns>
        public MabelResponse RegisterPrinter(int printerId, string printerName, string printerLocation, PrinterInfo info)
        {
            MabelResponse response = MakeRequest(
                new MabelRequest(
                    this,
                    "cardPrinter.register", 
                    new MabelPrinterRegisterParams( printerId ,printerName ,printerLocation,info)
                 )
            );
            return response;
        }

        /// <summary>
        /// Unregisters a printer
        /// </summary>
        /// <param name="printerId">The printer's ID</param>
        /// <returns></returns>
        public MabelResponse UnregisterPrinter(int printerId)
        {
            MabelResponse response = MakeRequest(
                new MabelRequest(
                    this,
                    "cardPrinter.unregister", 
                    new MabelPrinterUnregisterParams(printerId)
                   )
            );
            return response;
        }
        /// <summary>
        /// Checks the version of the API (not implemented)
        /// </summary>
        /// <returns></returns>
        public MabelResponse CheckVersion()
        {
            MabelResponse response = 
                MakeRequest(
                    new MabelRequest(
                        this,
                        "cardPrinter.checkVersion", 
                        null)
                 );
            return response;
        }
        /// <summary>
        /// Sets the status of a printer.
        /// </summary>
        /// <param name="printerId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public MabelResponse SetPrinterStatus(int printerId, string status)
        {
            MabelResponse response = 
                MakeRequest(new MabelRequest(
                    this,
                    "cardPrinter.printerSetStatus",
                    new MabelPrinterSetStatus(printerId,status)
                    )
                );
            return response;
        }

        /// <summary>
        /// Sets the status of a card via the API
        /// </summary>
        /// <param name="printerId"></param>
        /// <param name="card"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public MabelResponse SetCardStatus(int printerId, MabelCard card, string status)
        {
            MabelResponse response = MakeRequest(
                new MabelRequest(
                    this,
                    "cardHandler.setStatus",
                new MabelSetCardStatusParams(card.card_id, status)
                )
            );
            return response;

        }

        /// <summary>
        /// Sets a card's RFID via the API (deprecated)
        /// </summary>
        /// <param name="printerId"></param>
        /// <param name="card"></param>
        /// <param name="rfid"></param>
        /// <returns></returns>
        public MabelResponse SetCardRfid(int printerId, MabelCard card, String rfid)
        {
            MabelResponse response = MakeRequest(
                new MabelRequest(
                    this,
                    "cardPrinter.setStatus",
                    new MabelSetCardRfidParams( printerId, card.card_id, rfid)
                )
                );
            return response;
        }

        /// <summary>
        /// Sets a card's status to printed via the API.
        /// </summary>
        /// <param name="printerId"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public MabelResponse SetCardPrinted(int printerId, MabelCard card)
        {
            MabelResponse response = MakeRequest(new MabelRequest(this,"cardHandler.setPrinted", 
                new MabelSetCardPrinterParams( card.card_id)
                ));
            return response;

        }

        /// <summary>
        /// Sets the token on the last card that a printer requested.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="printerId"></param>
        /// <returns></returns>
        public MabelResponse SetToken(String token, int printerId)
        {
            MabelResponse response = MakeRequest(new MabelRequest(this, "cardHandler.capturedRFID",
                new MabelSetTokenParams(token,printerId)));
            return response;
        }

        /// <summary>
        /// Gets the next card enqueued to be printed
        /// </summary>
        /// <param name="printerId">The ID of the printer who is requesting the card</param>
        /// <returns></returns>
        public MabelCard GetNextJob(int printerId)
        {
            MabelRequest request = new MabelRequest(this, "cardHandler.getNextPrintJob", new MabelGetNextJobParams(printerId));
            MabelResponse response = MakeRequest(request);
            if (response.code == 200)
             {
                 if (response.results == null)
                 {
                     // The API is OK, there's just no cards to print :)
                     return null;
                 }
             } else
             {
                //Something has gone wrong, best ignore and hope for the best, but exit out this time around
                // throw an exception probably?
                throw new Exception("Problem with request to API: " + response.message);
             } 
             // otherwise...
             MabelCard card = new MabelCard();
             card.card_id = (int) response.results.SelectToken("cardId");
             card.member_id = response.results.SelectToken("memberId").ToString();

             card.card_front_image_encoded = (string)response.results.SelectToken("imageFront");
             card.card_back_image_encoded = (string)response.results.SelectToken("imageBack");
             return card;
        }

    }
}
