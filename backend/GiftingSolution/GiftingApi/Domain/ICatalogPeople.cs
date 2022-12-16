

public interface ICatalogPeople
{
    Task<PersonItemResponse> AddPersonAsync(PersonCreateRequest request);
    Task<PersonResponse> GetPeopleAsync();
    Task<PersonItemResponse?> GetPersonByIdAsync(int id);
}