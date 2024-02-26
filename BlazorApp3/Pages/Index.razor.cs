using CVClassLibrary.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp3.Pages
{
    public partial class Index
    {

        //--- Properties ---
        private Skill NewSkill { get; set; }
        private Skill SelectedSkill { get; set; }
        private Guid SelectedSkillId { get; set; }
        private Project NewProject { get; set; } = new Project();
        private List<Skill>? _listOfSkills;
        private List<Project>? _listOfProjects;
        private bool skillForm { get; set; }
        private bool projectForm { get; set; }
        private int ButtonPress { get; set; }
        string APIConnection { get; set; }

        private string selectedForm = "Skill"; // Default to Skill form

        //---Methods---

        //Handles the change of forms
        private void HandleFormToggle(ChangeEventArgs e)
        {
            selectedForm = e.Value.ToString();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                NewSkill = new();
                NewProject = new();
                ButtonPress = 0;
                APIConnection = Configuration.GetConnectionString("APIConnection");
                _listOfSkills = await apiCRUD.ListSkills(APIConnection);
                _listOfProjects = await apiCRUD.ListProjects(APIConnection);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task HandleButtonPress()
        {
            try
            {
                if (selectedForm == "Skill")
                {
                    switch (ButtonPress)
                    {
                        case 0:
                            await SubmitSkill();
                            break;
                        case 1:
                            await UpdateSkill();
                            break;
                        case 2:
                            await DeleteSkill();
                            break;
                    }

                } else
                {
                    switch (ButtonPress)
                    {
                        case 0:
                            await SubmitProject();
                            break;
                        case 1:
                            await UpdateProject();
                            break;
                        case 2:
                            await DeleteProject();
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //---------------- SKILLS ----------------------
        //Adds a skill to the database! (finally!!)
        private async Task SubmitSkill()
        {
            try
            {
                await apiCRUD.AddSkill(APIConnection, NewSkill);
                // NewSkill = new() { Id = Guid.NewGuid() };
                Console.WriteLine("SubmitSkill Complete");
                if (NewSkill is null)
                {

                }
                else
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
                    skillToUpdate.pictureURL = NewSkill.pictureURL;
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

        private async Task DeleteSkill()
        {
            try
            {
                bool response = await apiCRUD.DeleteSkill(APIConnection, NewSkill);
                if (response)
                {
                    var skillToDelete = _listOfSkills.Find(s => s.Id == NewSkill.Id);
                    if (skillToDelete != null)
                    {
                        _listOfSkills.Remove(skillToDelete);
                        Console.WriteLine("Delete Complete");
                        NewSkill = new();
                    }
                    else
                    {
                        await Console.Out.WriteLineAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                //Some sort of error logger?
                Console.WriteLine($"Error. {ex.Message}");
            }
        }

        //-------------- PROJECTS -----------------------------
        private async Task SubmitProject()
        {
            try
            {
                await apiCRUD.AddProject(APIConnection, NewProject);
                Console.WriteLine("SubmitSkill Complete");
                if (NewSkill is null)
                {

                }
                else
                {
                    _listOfProjects.Add(NewProject);
                }
                NewProject = new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. {ex.Message}");
            }
        }

        private async Task UpdateProject()
        {
            try
            {
                bool response = await apiCRUD.UpdateProject(APIConnection, NewProject);
                if (response)
                {

                    var ProjectToUpdate = _listOfProjects.Find(s => s.Id == NewProject.Id);

                    ProjectToUpdate.Title = NewProject.Title;
                    ProjectToUpdate.GithubLink = NewProject.GithubLink;
                    ProjectToUpdate.Description = NewProject.Description;

                    Console.WriteLine("SubmitSkill Complete");
                    NewProject = new();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. {ex.Message}");
            }
        }

        private async Task DeleteProject()
        {
            try
            {
                bool response = await apiCRUD.DeleteProject(APIConnection, NewProject);
                if (response)
                {
                    var ProjectToDelete = _listOfProjects.Find(s => s.Id == NewProject.Id);
                    if (ProjectToDelete != null)
                    {
                        _listOfProjects.Remove(ProjectToDelete);
                        Console.WriteLine("Delete Complete");
                        NewSkill = new();
                    }
                    else
                    {
                        await Console.Out.WriteLineAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. {ex.Message}");
            }
        }
    }
}
