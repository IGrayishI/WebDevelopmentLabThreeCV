using System.Net.Http.Json;

namespace CVClassLibrary.Models
{
    public class APICRUD
    {
        List<Skill> listOfSkills = new();
        List<Project> listOfProjects = new();
        public HttpClient httpClient = new();

        //-------------------SKILLS-----------------------

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

        public async Task<bool> DeleteSkill(string baseAdress, Skill skill)
        {
            try
            {
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                var respone = await httpClient.DeleteAsync($"/skills/{skill.Id}");
                return respone.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating skill: " + ex.Message);
                return false;
            }
        }

        //----------------PROJECTS-------------

        public async Task<List<Project>> ListProjects(string baseAddress)
        {
            try
            {
                // Create a new instance of HttpClient with the provided base address
                httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
                // Make GET request to your API endpoint
                var response = await httpClient.GetAsync("/projects");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response body into a list of products
                    listOfProjects = await response.Content.ReadFromJsonAsync<List<Project>>();
                    if (listOfProjects != null)
                        return listOfProjects;

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
                Console.WriteLine("An error occurred while retrieving projects: " + ex.Message);
                throw new Exception("Error while Handling");
            }
        }
        public async Task<bool> AddProject(string baseAdress, Project projects)
        {
            try
            {
                //using var httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                var response = await httpClient.PostAsJsonAsync("/project", projects);

                return response.IsSuccessStatusCode;


            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                Console.WriteLine("An error occurred while retrieving projects: " + ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateProject(string baseAdress, Project Project)
        {
            try
            {
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                var respone = await httpClient.PutAsJsonAsync($"/projects/{Project.Id}", Project);
                return respone.IsSuccessStatusCode;
            } catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating projects: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProject(string baseAdress, Project Project)
        {
            try
            {
                httpClient = new HttpClient { BaseAddress = new Uri(baseAdress) };
                var respone = await httpClient.DeleteAsync($"/projects/{Project.Id}");
                return respone.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating projects: " + ex.Message);
                return false;
            }
        }
    }
}
