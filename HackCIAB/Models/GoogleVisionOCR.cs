using System;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using System.Collections.Generic;
using System.Configuration;

namespace HackCIAB.Models
{
    public class GoogleVisionOCR
    {
        static string ArquivoCredencialGoogle = @"\auth_google.json";

        public static void RetornaImagemProduto(string CaminhoImagem)
        {
            string retorno = String.Empty;
            ImageAnnotatorClient client;
            BatchAnnotateImagesResponse responseArray;

            var credential = GoogleCredential.FromFile(ArquivoCredencialGoogle)
            .CreateScoped(ImageAnnotatorClient.DefaultScopes);
            var channel = new Grpc.Core.Channel(
                ImageAnnotatorClient.DefaultEndpoint.ToString(),
                credential.ToChannelCredentials());

            List<AnnotateImageRequest> requestArray = new List<AnnotateImageRequest>();
            int i = 1;
            int j = 0;
            int countRequest = 0;

            foreach (NotaFiscalOCR nota in lstNota)
            {
                try
                {
                    if (File.Exists(nota.Foto))
                    {
                        AnnotateImageRequest request = new AnnotateImageRequest
                        {
                            Image = Image.FromFile(produto.CaminhoImagemLocal),
                            Features = { new Feature { Type = Feature.Types.Type.DocumentTextDetection } }
                        };

                        requestArray.Add(request);
                    }

                    if (i == 15)
                    {
                        countRequest += 15;
                        Console.Write("\tFazendo requests " + countRequest + "/" + lstProduto.Count + "...");
                        Console.Write("\r");

                        client = ImageAnnotatorClient.Create(channel);

                        responseArray = client.BatchAnnotateImages(requestArray.ToArray());

                        foreach (AnnotateImageResponse response in responseArray.Responses)
                        {
                            
                            j++;
                        }

                        requestArray = new List<AnnotateImageRequest>();
                        i = 0;
                    }

                    i++;
                }
                catch (Exception ex)
                {

                }
            }

            client = ImageAnnotatorClient.Create(channel);

            responseArray = client.BatchAnnotateImages(requestArray.ToArray());

            foreach (AnnotateImageResponse response in responseArray.Responses)
            {
                if (response.Error == null && response.FullTextAnnotation != null)
                {
                    
                }

                j++;
            }
        }
    }

}
}