using GiftingApi.Adapters;
using Microsoft.EntityFrameworkCore;

namespace GiftingApi.Domain
{
    public class EfPeopleCatalog : ICatalogPeople
    {
        private readonly GiftingDataContext _context;

        public EfPeopleCatalog(GiftingDataContext context)
        {
            _context = context;
        }

        public async Task<PersonResponse> GetPeopleAsync()
        {
            // Select Id, FirstName, LastName from People where Unfriended = 0
            var data = await GetPeopleThatAreStillFriends().
                Select(p => new PersonItemResponse(p.Id.ToString(), p.FirstName, p.LastName)).ToListAsync();

            return new PersonResponse(data!);
        }

        public async Task<PersonItemResponse> AddPersonAsync(PersonCreateRequest request)
        {
            var personToAdd = new PersonEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
                UnFreinded = false
            };
            _context.People.Add(personToAdd);
            await _context.SaveChangesAsync();

            return new PersonItemResponse(personToAdd.Id.ToString(), personToAdd.FirstName, personToAdd.LastName);
        }



        public async Task<PersonItemResponse?> GetPersonByIdAsync(int id)
        {
            return await GetPeopleThatAreStillFriends()
                .Where(p => p.Id == id)
                .Select(p => new PersonItemResponse(p.FirstName, p.LastName, p.Id.ToString()))
                .SingleOrDefaultAsync();
        }





        private IQueryable<PersonEntity> GetPeopleThatAreStillFriends()
        {
            return _context.People.Where(p => p.UnFreinded == false).OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
        }
    }
}
