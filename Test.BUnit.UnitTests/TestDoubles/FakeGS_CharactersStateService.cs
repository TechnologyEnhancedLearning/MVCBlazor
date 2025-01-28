using AutoFixture;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.StateServices.CharacterStateServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BUnit.UnitTests.TestDoubles
{
    public class FakeGS_CharactersStateService : IGS_CharactersStateService
    {
        private readonly List<GE_CharacterModel> characters;
        private readonly TaskCompletionSource<GE_CharacterModel> characterAdded = new();

        public event Action CharactersChanged;

        public FakeGS_CharactersStateService()
        {
            // Use AutoFixture to create test data
            var fixture = new Fixture();
            this.characters = fixture.CreateMany<GE_CharacterModel>(5).ToList(); // 5 initial characters
        }



        public Task<GE_ServiceResponse<List<GE_CharacterModel>>> GetCharactersAsyncWithResponse()
        {
            return Task.FromResult(new GE_ServiceResponse<List<GE_CharacterModel>>()
            {
                Data = characters,
                Success = true
            });
        }

        public Task<GE_ServiceResponse<bool>> SetCharacterAsFavouriteAsync(int characterId)
        {
            var character = characters.FirstOrDefault(c => c.Id == characterId);
            if (character != null)
            {
                // Simulate marking as favorite (no real change to data, just return true)
                return Task.FromResult(new GE_ServiceResponse<bool> { Data = true, Success = true });
            }
            else
            {
                return Task.FromResult(new GE_ServiceResponse<bool> { Data = false, Success = false });
            }
        }


        public Task<GE_ServiceResponse<bool>> AddCharacterAsync(GE_CharacterModel character)
        {
            // Simulate adding the character and returning the updated list
            characters.Add(character);

            return Task.FromResult(new GE_ServiceResponse<bool> { Data = true, Success = true });
        }

        public Task<GE_ServiceResponse<bool>> RemoveCharacterByTemporaryIdAsync(Guid clientTemporaryId)
        {
            var characterToRemove = characters.Single(c => c.ClientTemporaryId == clientTemporaryId);
            if (characterToRemove != null)
            {
                characters.Remove(characterToRemove);
                return Task.FromResult(new GE_ServiceResponse<bool> { Data = true, Success = true });
            }

            return Task.FromResult(new GE_ServiceResponse<bool> { Data = false, Success = false });
        }


        public Task<GE_ServiceResponse<List<GE_CharacterModel>>> ReplaceDBWithListAsync()
        {
            return Task.FromResult(new GE_ServiceResponse<List<GE_CharacterModel>> { Data = characters, Success = true });
        }

        public Task EnsureDataIsLoadedAsync()
        {
            return Task.CompletedTask;
        }

        public Task<GE_ServiceResponse<List<GE_CharacterModel>>> GetCharactersAsync()
        {
            return Task.FromResult(new GE_ServiceResponse<List<GE_CharacterModel>>()
            {
                Data = characters,
                Success = true
            });
        }

        public bool DataIsLoaded => true;

        public List<GE_CharacterModel> Characters => characters;
    }
}
