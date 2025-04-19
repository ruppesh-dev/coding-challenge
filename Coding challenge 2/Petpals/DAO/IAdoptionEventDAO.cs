    using System;
    using System.Collections.Generic;
    using PetPalsManagementSystem.Entity;

    namespace PetPalsManagementSystem.dao
    {
        public interface IAdoptionEventDAO
        {
            List<AdoptionEvent> GetUpcomingEvents();
            void RegisterParticipant(string participantName, string participantType, int eventId);
        }
    }
