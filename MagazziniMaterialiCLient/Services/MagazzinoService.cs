using MagazziniMaterialiCLient.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace MagazziniMaterialiCLient.Services
{
    public class MagazzinoService
    {
        private readonly HttpClient _httpClient;

        public MagazzinoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Magazzino>> GetMagazziniAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Magazzino>>("Magazzino");
        }

        public async Task<Magazzino> GetMagazzinoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Magazzino>($"Magazzino/{id}");
        }

        public async Task<Magazzino> AddMagazzinoAsync(Magazzino magazzino)
        {
            var response = await _httpClient.PostAsJsonAsync("Magazzino", magazzino);
            return await response.Content.ReadFromJsonAsync<Magazzino>();
        }

        public async Task<Magazzino> EditMagazzinoAsync(int id, Magazzino magazzino)
        {
            var response = await _httpClient.PutAsJsonAsync($"Magazzino/{id}", magazzino);
            return await response.Content.ReadFromJsonAsync<Magazzino>();
        }

        public async Task DeleteMagazzinoAsync(int id)
        {
            await _httpClient.DeleteAsync($"Magazzino/{id}");
        }

        public async Task<List<Materiale>> GetMaterialiByMagazzinoIdAsync(int magazzinoId)
        {
            return await _httpClient.GetFromJsonAsync<List<Materiale>>($"Magazzino/MaterialiByMagazzino/{magazzinoId}");
        }

        public async Task RegisterMaterialeMagazzinoAsync(int magazzinoId, int materialeId)
        {
            await _httpClient.PostAsync($"Magazzino/registerMaterialeMagazzino/{magazzinoId}/{materialeId}", null);
        }

        public async Task DeleteMaterialeMagazzinoAsync(int magazzinoId, int materialeId)
        {
            await _httpClient.DeleteAsync($"Magazzino/registerMaterialeMagazzino/{magazzinoId}/{materialeId}");
        }
    }
}
