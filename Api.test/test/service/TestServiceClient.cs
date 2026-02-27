using Api.core.Application.repository;
using Api.core.Application.service;
using Xunit;

public class Test
{
    static Host host = new();
   static RepositoryClient repo = new(host);
    private ClientService _service = new(repo);
    [Fact]
    public async Task TestGetAll()
    {
        string nome = "Debora";
        string cpf = "614.038.970-42";
        await _service.AddService(nome, cpf, 345, true);
        await _service.GetAllService();
      int id=  await _service.GetIdService(cpf);
        await _service.DeleteService(id);
    }
}