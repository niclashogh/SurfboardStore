using Lib.Products;
using Rental.Data;
using System.Text.Json;

namespace Rental.Services.ApiCall
{
    public class SurfboardApi : IApi<Surfboard>
    {
        #region Dependency Injections
        private readonly HttpClient httpClient = new();
        //private readonly RentalContext rentalContext; //Eliminate the need for SQL Interaction/Injection

        public SurfboardApi(/*RentalContext rentalContext*/)
        {
            //this.rentalContext = rentalContext;
        }
        #endregion

        #region AddAsync
        public async Task AddAsync(string apiUrl, Surfboard surfboard)
        {
            try
            {
                if (surfboard == null)
                {
                    // Message to API (We need to supply the API with the correct URL + Query > Not sure if this is correct.)
                    var jsonSerializedModel = JsonSerializer.Serialize(surfboard);
                    await httpClient.PostAsJsonAsync(apiUrl, jsonSerializedModel);
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region EditAsync
        public async Task EditAsync(string apiUrl, Surfboard surfboard)
        {
            try
            {
                if (surfboard != null)
                {
                    // Message to API (We need to supply the API with the correct URL + Query > Not sure if this is correct.)
                    var jsonSerializedModel = JsonSerializer.Serialize(surfboard);
                    await httpClient.PostAsJsonAsync(apiUrl, jsonSerializedModel);
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region DeleteAsync
        public async Task DeleteAsync(string apiUrl, int id)
        {
            try
            {
                //Surfboard? selected = await rentalContext.Surfboard.FindAsync(id); //Is it redundant to validate model here? (The meaning is to eliminate the need to use SQL server from elsewhere than the API)

                if (id != null)
                {
                    // Message to API (We need to supply the API with the correct URL + Query > Not sure if this is correct.)
                    var jsonSerializedModelId = JsonSerializer.Serialize(id);
                    await httpClient.PostAsJsonAsync(apiUrl, jsonSerializedModelId);
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion

        #region GetAllAsync
        public async Task<object> GetAllAsync(string apiUrl)
        {
            try
            {
                // Awating response from API
                var apiResponse = await httpClient.GetAsync(apiUrl);

                if (apiResponse != null)
                {
                    try
                    {
                        //Converting the response to a valid return type
                        var jsonSerialize = await apiResponse.Content.ReadAsStringAsync();
                        IEnumerable<Surfboard> deserializedModel = JsonSerializer.Deserialize<IEnumerable<Surfboard>>(jsonSerialize);

                        return deserializedModel;
                    }
                    catch // If it fails to convert the response into a Surfboard model, it is possible that the API is returning an error in string format
                    {
                        return await apiResponse.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    return "The api response was empty";
                }
            }
            catch
            {
                return "Failed doing the API call";
            }
        }
        #endregion
    }
}
