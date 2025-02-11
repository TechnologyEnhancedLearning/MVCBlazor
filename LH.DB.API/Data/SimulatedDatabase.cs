using Package.LH.Entities.Models;
using Package.Shared.Entities.BaseClasses;
using Package.Shared.Entities.Interfaces;
using Package.Shared.Entities.Models;
using System;

namespace LH.DB.API.Data
{
    public  class SimulatedDatabase : ISimulatedDatabase
    {

        public List<GE_CartoonModel> Cartoons { get; private set; }
        public List<LH_MeetingModel> Meetings { get; private set; }
        public string ClickCount { get; set; } = "0";
        public List<string> Logs { get; set; } = new List<string>();

        public SimulatedDatabase()
        {
            Cartoons = new List<GE_CartoonModel>();
            Meetings = new List<LH_MeetingModel>();
            PrepopulateCartoonsAndCharacters(); // Populate cartoons and characters
            PrepopulateMeetingsAndAttendees();   // Populate meetings and attendees
        }

        public void AddCharacterToCartoon(int cartoonId, GE_CharacterModel character)
        {
            Cartoons.Single(x=>cartoonId == x.Id).People.Add(character);

            ReassignListGroupPeopleIds<GE_CharacterModel>(Cartoons.Cast<GE_GroupBase<GE_CharacterModel>>().ToList());

        }
        public void AddAttendeeToMeeting(int meetingId, LH_AttendeeModel attendee)
        {
            Meetings.Single(x => meetingId == x.Id).People.Add(attendee);

            ReassignListGroupPeopleIds<LH_AttendeeModel>(Meetings.Cast<GE_GroupBase<LH_AttendeeModel>>().ToList()); 

        }



        public void ReassignListGroupPeopleIds<T>(List<GE_GroupBase<T>> groups) where T : IGE_Person
        {
            foreach (GE_GroupBase<T>  group in groups)
            {
                // Reassign Ids for each person in the group
                for (int i = 0; i < group.People.Count; i++)
                {
                    // Assuming IGE_Person has an Id property that can be set
                    group.People[i].Id = i + 1;
                }
            }
        }
   
        private void PrepopulateCartoonsAndCharacters()
        {
            // Create cartoons and add characters to them
            var adventureTime = new GE_CartoonModel(1, "Adventure Time", false, new List<GE_CharacterModel>());
            adventureTime.People.Add(new GE_CharacterModel(100, "Finn", "the Human", false, false, "https://example.com/finn.png"/*, adventureTime*/));
            adventureTime.People.Add(new GE_CharacterModel(200, "Jake", "the Dog", false, false, "https://example.com/jake.png"/*, adventureTime*/));
            adventureTime.People.Add(new GE_CharacterModel(300, "Princess", "Bubblegum", false, false, "https://example.com/bubblegum.png"/*, adventureTime*/));
            adventureTime.People.Add(new GE_CharacterModel(400, "Marceline", "the Vampire Queen", false, true, "https://example.com/marceline.png"/*, adventureTime*/));
            adventureTime.People.Add(new GE_CharacterModel(500, "Ice", "King", false, false, "https://example.com/iceking.png"/*, adventureTime*/));
            Cartoons.Add(adventureTime);

            var shera = new GE_CartoonModel(2, "She-Ra: Princesses of Power", false, new List<GE_CharacterModel>());
            shera.People.Add(new GE_CharacterModel(6, "Adora", "She-Ra", false, false, "https://example.com/shera.png"/*, shera*/));
            shera.People.Add(new GE_CharacterModel(7, "Catra", "of the Horde", false, false, "https://example.com/catra.png"/*, shera*/));
            shera.People.Add(new GE_CharacterModel(8, "Glimmer", "of the Rebellion", false, false, "https://example.com/glimmer.png"/*, shera*/));
            shera.People.Add(new GE_CharacterModel(9, "Bow", "of the Rebellion", false, false, "https://example.com/bow.png"/*, shera*/));
            shera.People.Add(new GE_CharacterModel(10, "Scorpia", "of the Horde", false, false, "https://example.com/scorpia.png"/*, shera*/));
            shera.People.Add(new GE_CharacterModel(11, "Entrapta", "of the Rebellion", false, false, "https://example.com/entrapta.png"/*, shera*/));
            shera.People.Add(new GE_CharacterModel(12, "Shadow Weaver", "of the Horde", false, true, "https://example.com/shadowweaver.png"/*, shera*/));
            Cartoons.Add(shera);

            var edgerunners = new GE_CartoonModel(3, "Edgerunners", false, new List<GE_CharacterModel>());
            edgerunners.People.Add(new GE_CharacterModel(13, "David", "Martinez", false, false, "https://example.com/david.png"/*, edgerunners*/));
            edgerunners.People.Add(new GE_CharacterModel(14, "Lucy", "of the Edgerunner Gang", false, true, "https://example.com/lucy.png"/*, edgerunners*/));
            Cartoons.Add(edgerunners);


        }
        private void PrepopulateMeetingsAndAttendees()
        {
            // Create meetings and add attendees to them
            var workMeeting = new LH_MeetingModel(1, "Work Meeting", false,new List<LH_AttendeeModel>());
            workMeeting.People.Add(new LH_AttendeeModel(1, "Ann", "Johnson", false, "Participant"/*, workMeeting*/));
            workMeeting.People.Add(new LH_AttendeeModel(2, "Bob", "Smith", false, "Participant"/*, workMeeting*/));
            workMeeting.People.Add(new LH_AttendeeModel(5, "Chao", "Zhang", false, "Observer"/*, workMeeting*/));
            workMeeting.People.Add(new LH_AttendeeModel(6, "Fatima", "Al-Farsi", false, "Participant"/*, workMeeting*/));
            workMeeting.People.Add(new LH_AttendeeModel(7, "Rajesh", "Kumar", false, "Presenter"/*, workMeeting*/));
            Meetings.Add(workMeeting);

            // Social Meeting
            var socialMeeting = new LH_MeetingModel(2, "Social Meeting", false, new List<LH_AttendeeModel>());
            socialMeeting.People.Add(new LH_AttendeeModel(3, "Greg", "Brown", false, "Participant"/*, socialMeeting*/));
            socialMeeting.People.Add(new LH_AttendeeModel(4, "Sarah", "Wilson", false, "Participant"/*, socialMeeting*/));
            socialMeeting.People.Add(new LH_AttendeeModel(8, "Aisha", "Nguyen", false, "Host"/*, socialMeeting*/));
            socialMeeting.People.Add(new LH_AttendeeModel(9, "Tariq", "Hassan", false, "Participant"/*, socialMeeting*/));
            socialMeeting.People.Add(new LH_AttendeeModel(10, "Leila", "Santos", false, "Observer"/*, socialMeeting*/));
            Meetings.Add(socialMeeting);
        }


    }
}
