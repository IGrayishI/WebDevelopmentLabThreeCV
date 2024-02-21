using System.Net.Http.Json;

namespace CVClassLibrary.Models
{
    public class APICRUD
    {
        List<Skill> listOfSkills = new();
        public HttpClient httpClient = new();

        public async Task<List<Skill>> ListSkills(string baseAdress)
        {
            try
            {
                // Create a new instance of HttpClient with the provided base address
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                // Make GET request to your API endpoint
                var response = await httpClient.GetAsync("/skills");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response body into a list of products
                    listOfSkills = await response.Content.ReadFromJsonAsync<List<Skill>>();
                    if (listOfSkills != null)
                        return listOfSkills;

                    else
                        throw new Exception("List is Empty");

                }
                else
                {
                    //a bad attempt at handling an error.
                    Console.WriteLine("Failed to retrieve products. Status code: " + response.StatusCode);
                    throw new Exception("response dont have a successful code");
                }
            }
            catch (Exception ex)
            {
                //a bad attempt at handling an error.
                Console.WriteLine("An error occurred while retrieving skills: " + ex.Message);
                throw new Exception("Error while Handling");
            }
        }

        public async Task<bool> AddSkill(string baseAdress, Skill skill)
        {
            try
            {
                //using var httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                var response = await httpClient.PostAsJsonAsync("/skill", skill);

                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                Console.WriteLine("An error occurred while retrieving skills: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateSkill(string baseAdress, Skill skill)
        {
            try
            {
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                var respone = await httpClient.PutAsJsonAsync($"/skills/{skill.Id}", skill);
                return respone.IsSuccessStatusCode;
            } catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating skill: " + ex.Message);
                return false;
            }
        }
    }
}
