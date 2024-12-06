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
        //public void AddAttendeeToMeeting(int meetingId, LH_AttendeeModel attendee)
        //{
        //    Meetings.Single(x => meetingId == x.Id).People.Add(attendee);
        //    ReassignIds<LH_AttendeeModel>(Meetings); // Reassign IDs after adding the character
        //}
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
           // ReassignCartoonsIds(Cartoons); // Reassign IDs after adding the character
           // ReassignListGroupPeopleIds<GE_CharacterModel>(Cartoons); //GE_PersonBase
            ReassignListGroupPeopleIds<GE_CharacterModel>(Cartoons.Cast<GE_GroupBase<GE_CharacterModel>>().ToList());

            //        'System.Collections.Generic.List<Package.Shared.Entities.Models.GE_CartoonModel>' to 
            //'System.Collections.Generic.List<Package.Shared.Entities.BaseClasses.GE_GroupBase<Package.Shared.Entities.Models.GE_CharacterModel>>'  


        }
        public void AddAttendeeToMeeting(int meetingId, LH_AttendeeModel attendee)
        {
            Meetings.Single(x => meetingId == x.Id).People.Add(attendee);

            ReassignListGroupPeopleIds<LH_AttendeeModel>(Meetings.Cast<GE_GroupBase<LH_AttendeeModel>>().ToList()); 

        }


        private void ReassignCartoonsIds(List<GE_CartoonModel> listToReassign)
        {
            foreach (GE_CartoonModel group in listToReassign)
            {
                // Reassign Ids for each person in the group
                for (int i = 0; i < group.People.Count; i++)
                {
                    // Assuming IGE_Person has an Id property that can be set
                    group.People[i].Id = i + 1;
                }
            }
        }

        //Public because not being a real db its not adding it own ids etc
        public void ReassignListGroupPeopleIds<T>(List<GE_GroupBase<T>> groups) where T : IGE_Person
        {
            foreach (GE_GroupBase<T>  group in groups)
            {
                ReassignGroupPeopleIds<T>(group);
            }
        }
        private void ReassignGroupPeopleIds<T>(GE_GroupBase<T> group) where T : IGE_Person
        {

                ReassignPeopleIds<T>(group.People);
         
        }
        private void ReassignPeopleIds<T>(List<T> people) where T : IGE_Person
        {
      
                // Reassign Ids for each person in the group
                for (int i = 0; i < people.Count; i++)
                {
                    // Assuming IGE_Person has an Id property that can be set
                    people[i].Id = i + 1;
                }
            
        }

        private void ReassignIds<T, TGroup>(List<TGroup> listToReassign)
            where T : IGE_Person
            where TGroup : GE_GroupBase<T>
       // private void ReassignIds<T>(List<GE_GroupBase<T>> listToReassign) where T : GE_PersonBase
        {
            foreach (var group in listToReassign)
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

            // Assign IDs based on character position in their respective cartoon
            //AssignCharacterIds();
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
