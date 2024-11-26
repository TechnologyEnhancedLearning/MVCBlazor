namespace LH.DB.API.Data
{
    public class SimulatedDatabase : ISimulatedDatabase
    {

        private List<GE_CartoonModel> cartoons;
        private List<MeetingModel> meetings;

        public SimulatedDatabase()
        {
            cartoons = new List<GE_CartoonModel>();
            meetings = new List<MeetingModel>();
            AddCartoonsAndCharacters(); // Populate cartoons and characters
            AddMeetingsAndAttendees();   // Populate meetings and attendees
        }

        private void AddCartoonsAndCharacters()
        {
            // Create cartoons and add characters to them
            var adventureTime = new GE_CartoonModel(1, "Adventure Time", false);
            adventureTime.Characters.Add(new CartoonCharacterModel(100, "Finn", "the Human", false, false, "https://example.com/finn.png"/*, adventureTime*/));
            adventureTime.Characters.Add(new CartoonCharacterModel(200, "Jake", "the Dog", false, false, "https://example.com/jake.png"/*, adventureTime*/));
            adventureTime.Characters.Add(new CartoonCharacterModel(300, "Princess", "Bubblegum", false, false, "https://example.com/bubblegum.png"/*, adventureTime*/));
            adventureTime.Characters.Add(new CartoonCharacterModel(400, "Marceline", "the Vampire Queen", false, true, "https://example.com/marceline.png"/*, adventureTime*/));
            adventureTime.Characters.Add(new CartoonCharacterModel(500, "Ice", "King", false, false, "https://example.com/iceking.png"/*, adventureTime*/));
            cartoons.Add(adventureTime);

            var shera = new GE_CartoonModel(2, "She-Ra: Princesses of Power", false);
            shera.Characters.Add(new CartoonCharacterModel(6, "Adora", "She-Ra", false, false, "https://example.com/shera.png"/*, shera*/));
            shera.Characters.Add(new CartoonCharacterModel(7, "Catra", "of the Horde", false, false, "https://example.com/catra.png"/*, shera*/));
            shera.Characters.Add(new CartoonCharacterModel(8, "Glimmer", "of the Rebellion", false, false, "https://example.com/glimmer.png"/*, shera*/));
            shera.Characters.Add(new CartoonCharacterModel(9, "Bow", "of the Rebellion", false, false, "https://example.com/bow.png"/*, shera*/));
            shera.Characters.Add(new CartoonCharacterModel(10, "Scorpia", "of the Horde", false, false, "https://example.com/scorpia.png"/*, shera*/));
            shera.Characters.Add(new CartoonCharacterModel(11, "Entrapta", "of the Rebellion", false, false, "https://example.com/entrapta.png"/*, shera*/));
            shera.Characters.Add(new CartoonCharacterModel(12, "Shadow Weaver", "of the Horde", false, true, "https://example.com/shadowweaver.png"/*, shera*/));
            cartoons.Add(shera);

            var edgerunners = new GE_CartoonModel(3, "Edgerunners", false);
            edgerunners.Characters.Add(new CartoonCharacterModel(13, "David", "Martinez", false, false, "https://example.com/david.png"/*, edgerunners*/));
            edgerunners.Characters.Add(new CartoonCharacterModel(14, "Lucy", "of the Edgerunner Gang", false, true, "https://example.com/lucy.png"/*, edgerunners*/));
            cartoons.Add(edgerunners);

            // Assign IDs based on character position in their respective cartoon
            //AssignCharacterIds();
        }

        private void AssignCharacterIds()
        {
            foreach (var cartoon in cartoons)
            {
                for (int i = 0; i < cartoon.Characters.Count; i++)
                {
                    cartoon.Characters[i].Id = i + 1; // Set Id based on index
                }
            }
        }

        public void AddCharacterToCartoon(GE_CartoonModel cartoon, CartoonCharacterModel character)
        {
            cartoon.Characters.Add(character);
            AssignCharacterIds(); // Reassign IDs after adding the character
        }

        public List<GE_CartoonModel> Cartoons => cartoons; // Expose cartoons for external access

        private void AddMeetingsAndAttendees()
        {
            // Create meetings and add attendees to them
            var workMeeting = new MeetingModel(1, "Work Meeting", false);
            workMeeting.Attendees.Add(new LH_AttendeeModel(1, "Ann", "Johnson", false, "Participant"/*, workMeeting*/));
            workMeeting.Attendees.Add(new LH_AttendeeModel(2, "Bob", "Smith", false, "Participant"/*, workMeeting*/));
            workMeeting.Attendees.Add(new LH_AttendeeModel(5, "Chao", "Zhang", false, "Observer"/*, workMeeting*/));
            workMeeting.Attendees.Add(new LH_AttendeeModel(6, "Fatima", "Al-Farsi", false, "Participant"/*, workMeeting*/));
            workMeeting.Attendees.Add(new LH_AttendeeModel(7, "Rajesh", "Kumar", false, "Presenter"/*, workMeeting*/));
            meetings.Add(workMeeting);

            // Social Meeting
            var socialMeeting = new MeetingModel(2, "Social Meeting", false);
            socialMeeting.Attendees.Add(new LH_AttendeeModel(3, "Greg", "Brown", false, "Participant"/*, socialMeeting*/));
            socialMeeting.Attendees.Add(new LH_AttendeeModel(4, "Sarah", "Wilson", false, "Participant"/*, socialMeeting*/));
            socialMeeting.Attendees.Add(new LH_AttendeeModel(8, "Aisha", "Nguyen", false, "Host"/*, socialMeeting*/));
            socialMeeting.Attendees.Add(new LH_AttendeeModel(9, "Tariq", "Hassan", false, "Participant"/*, socialMeeting*/));
            socialMeeting.Attendees.Add(new LH_AttendeeModel(10, "Leila", "Santos", false, "Observer"/*, socialMeeting*/));
            meetings.Add(socialMeeting);
        }

        public List<MeetingModel> Meetings => meetings; // Expose meetings for external access

    }
}
