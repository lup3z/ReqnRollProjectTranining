using Newtonsoft.Json;
using SpecFlowProjectAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace SpecFlowProjectAPI.StepDefinitions
{
    [Binding]
    public class ReqnRollStepDefinition
    {
        private readonly HttpClient _httpClient;
        private ReqnRollApi _responseData; // Variable estática para almacenar la respuesta


        public ReqnRollStepDefinition()
        {
            _httpClient = new HttpClient();
        }

        private async Task<ReqnRollApi> GetApiResponse(string url)
        {
            int retryCount = 0;
            const int maxRetries = 5;
            const int delay = 1000; // 1 segundo

            while (true)
            {
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    retryCount++;
                    if (retryCount > maxRetries)
                    {
                        Assert.Fail("Número máximo de reintentos alcanzado debido a 'Too Many Requests'.");
                    }
                    await Task.Delay(delay); // Esperar antes de volver a intentar
                    continue; // Volver a intentar la solicitud
                }

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta en la clase ReqnRollApi
                return JsonConvert.DeserializeObject<ReqnRollApi>(responseBody);
            }
        }

        
        [Given(@"el usuario envía una solicitud GET a ""(.*)""")]
        public async Task GivenElUsuarioEnviaUnaSolicitudGETA(string url)
        {
            // Guardar la respuesta en la variable estática
            _responseData = await GetApiResponse(url);

        }

       
        [Then(@"la respuesta debería contener al menos una ""correct_answer"" con valor ""True""")]
        public void ThenLaRespuestaDeberiaContenerAlMenosUnaCorrectAnswerConValorTrue()
        {
            // Validar que exista al menos un "correct_answer" con valor "True"
            bool existsTrueAnswer = _responseData.results.Any(r => r.correct_answer == "True");
            Assert.IsTrue(existsTrueAnswer, "No se encontró 'correct_answer': 'True' en la respuesta.");
        }

      
        [Then(@"todas las preguntas deberían tener exactamente 3 respuestas incorrectas")]
        public void ThenTodasLasPreguntasDeberianTenerExactamente3RespuestasIncorrectas()
        {
            // Validar que todas las preguntas tengan 3 respuestas incorrectas
            bool allHaveThreeIncorrectAnswers = _responseData.results.All(r => r.incorrect_answers.Length == 3);
            Assert.IsTrue(allHaveThreeIncorrectAnswers, "No todas las preguntas tienen exactamente 3 respuestas incorrectas.");
        }

      
        [Then(@"mostrar las preguntas donde ""correct_answer"" es ""True""")]
        public void ThenMostrarLasPreguntasDondeCorrectAnswerEsTrue()
        {
            // Filtrar y mostrar las preguntas donde "correct_answer" es "True"
            var questionsWithTrueAnswer = _responseData.results
                .Where(r => r.correct_answer == "True")
                .Select(r => r.question);

            Console.WriteLine("Preguntas donde 'correct_answer' es 'True':");
            foreach (var question in questionsWithTrueAnswer)
            {
                Console.WriteLine(question);
            }
        }

        
        [Then(@"extraer y mostrar la ""correct_answer"" para la pregunta ""(.*)""")]
        public void ThenExtraerYMostrarLaCorrectAnswerParaLaPregunta(string specificQuestion)
        {
            // Buscar la pregunta específica y extraer la "correct_answer"
            var question = _responseData.results.FirstOrDefault(r => r.question.Contains(specificQuestion));

            if (question != null)
            {
                Console.WriteLine($"La respuesta correcta para la pregunta '{specificQuestion}' es: {question.correct_answer}");
            }
            else
            {
                Assert.Fail($"No se encontró la pregunta '{specificQuestion}' en la respuesta.");
            }
        }
    }
}