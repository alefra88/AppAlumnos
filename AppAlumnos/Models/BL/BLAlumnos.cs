using AppAlumnos.Models.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AppAlumnos.Models.BL
{
    public class BLAlumnos
    {
        private string _urlWebAPI;
        public BLAlumnos()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlWebAPI = builder.GetSection("urlWebAPI").Value;
        }
        public async Task<List<Alumnos>> Consultar()
        {
            var alumnos = new List<Alumnos>();
            try
            {
                using (var client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_urlWebAPI);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        alumnos = JsonConvert.DeserializeObject<List<Alumnos>>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondio con error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"WebAPi respondió con error {ex.Message}");
                }
            }
            return alumnos;


        }
        public async Task<Alumnos> Consultar(int? id)
        {
            var alumnos = new Alumnos();
            try
            {
                using (var client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(_urlWebAPI + $"/{id}");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        alumnos = JsonConvert.DeserializeObject<Alumnos>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondio con error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"WebAPi respondió con error {ex.Message}");
                }
            }
            return alumnos;
        }
        public async Task<Alumnos> Agregar(Alumnos alumnos)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(alumnos), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var responseTask = await client.PostAsync(_urlWebAPI, httpContent);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        alumnos = JsonConvert.DeserializeObject<Alumnos>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebApi respondió con el siguiente error {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebApi respondió con el siguiente error {ex.Message}");
            }
            return alumnos;
        }
        public async Task Actualizar(Alumnos alumnos) 
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(alumnos), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var responseTask = await client.PutAsync(_urlWebAPI+$"/{alumnos.Id}", httpContent);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        alumnos = JsonConvert.DeserializeObject<Alumnos>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebApi respondió con el siguiente error {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebApi respondió con el siguiente error {ex.Message}");
            }
          
        }
        public async Task Eliminar(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseTask = await client.DeleteAsync(_urlWebAPI + $"/{id}");
                    if (!responseTask.IsSuccessStatusCode)
                    {
                        throw new Exception($"WebApi tuvo un error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebApi lanzo el siguiente error. {ex.Message}");
            }
        }
    }
}
