using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiHelper.Models;
using System.Net.Http;
using System;

namespace ApiHelper
{
    public class DogApiProcessor
    {
        

        public static async Task<BreedsModel> LoadBreedList()
        {
            ///TODO : À compléter LoadBreedList
            /// Attention le type de retour n'est pas nécessairement bon
            /// J'ai mis quelque chose pour avoir une base
            /// TODO : Compléter le modèle manquant

            string url;
            url = $"https://dog.ceo/api/breeds/list/all";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    BreedsModel breeds = await response.Content.ReadAsAsync<BreedsModel>();
                    return breeds;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        
        public static async Task<DogApiModel> GetImageUrl(string breed, string nbimg)
        {
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant
            string url;
            if (breed != "ALL")
                url = $"https://dog.ceo/api/breed/{breed}/images/random/{nbimg}";
            else
                url = $"https://dog.ceo/api/breeds/image/random/{nbimg}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogApiModel breedimg = await response.Content.ReadAsAsync<DogApiModel>();
                    return breedimg;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
