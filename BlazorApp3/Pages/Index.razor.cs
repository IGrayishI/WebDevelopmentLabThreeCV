using CVClassLibrary.Models;
using Microsoft.AspNetCore.Components;


namespace BlazorApp3.Pages
{
    public partial class Index
    {

        //--- Properties ---
        private Skill NewSkill { get; set; } = new Skill();
        private List<Skill>? _listOfSkills; 
        string APIConnection { get; set; }

        //---Methods---

        protected override async Task OnInitializedAsync()
        {

            try
            {
                NewSkill = new();
                APIConnection = Configuration.GetConnectionString("APIConnection");
                _listOfSkills = await apiCRUD.ListSkills(APIConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Adds a skill to the database! (finally!!)
        private async Task SubmitSkill()
        {
            try
            {
                await apiCRUD.AddSkill(APIConnection, NewSkill);
                // NewSkill = new() { Id = Guid.NewGuid() };
                Console.WriteLine("SubmitSkill Complete");
                if(NewSkill is null)
                {

                } else
                {
                    _listOfSkills.Add(NewSkill);
                }
                    NewSkill = new();
            }
            catch (Exception ex)
            {
                //Some sort of error logger?
                Console.WriteLine($"Error. {ex.Message}");
            }
        }

        private async Task UpdateSkill()
        {
            try
            {
                bool response = await apiCRUD.UpdateSkill(APIConnection, NewSkill);
                if (response)
                {

                    var skillToUpdate = _listOfSkills.Find(s => s.Id == NewSkill.Id);
                    
                    skillToUpdate.Title = NewSkill.Title;
                    skillToUpdate.Description = NewSkill.Description;
                    skillToUpdate.SkillLevel = NewSkill.SkillLevel;
                    skillToUpdate.YearsOfExperience = NewSkill.YearsOfExperience;
                    
                    Console.WriteLine("SubmitSkill Complete");
                    NewSkill = new();
                }
            }
            catch (Exception ex)
            {
                //Some sort of error logger?
                Console.WriteLine($"Error. {ex.Message}");
            }
        }
        
    }
}
