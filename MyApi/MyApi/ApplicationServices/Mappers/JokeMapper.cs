using MyApi.Dtos;
using MyApi.Models;

namespace MyApi.ApplicationServices.Mappers
{
    /// <summary>
    /// Mapper class for jokes
    /// </summary>
    public static class JokeMapper
    {
        /// <summary>
        /// Maps Joke to Joke data transfer objects.
        /// </summary>
        /// <param name="apiResponse">list of joke response</param>
        /// <returns>list of joke DTO</returns>
        public static List<JokeDto> ToJokeDtoList(List<JokeResponse> apiResponse)
        {
            var jokeList = new List<JokeDto>();

            foreach (var joke in apiResponse)
            {
                jokeList.Add(new JokeDto
                {
                    IconUrl = joke.IconUrl,
                    Value = joke.Value
                });
            }

            return jokeList;
        }
    }
}
