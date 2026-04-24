using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using HeroesApi.Models;
using HeroesApi.Data;
namespace HeroesApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HeroesController : ControllerBase {
    [HttpGet]
    public ActionResult<List<Hero>> GetAll([FromQuery] string? universe = null) {
        var heroes = HeroesStore.Heroes;
        if (!string.IsNullOrEmpty(universe)) {
            heroes = heroes.Where(h => h.Universe.ToString()== universe).ToList();
        }
        return Ok(heroes);
    }
    [HttpGet("{id}")]
    public ActionResult<Hero> GetById(int id) {
        var hero = HeroesStore.Heroes.FirstOrDefault(h => h.Id == id);
        if (hero is null) {
            return NotFound(new { message = $"Герой с id={id} не найдена"});
        }
        return Ok(hero);
    }
    [HttpGet("search")]
    public ActionResult<List<Hero>> Search([FromQuery] string name) {
        List<Hero> result = new List<Hero>();
    foreach (var hero in HeroesStore.Heroes)
    {
        if (hero.Name.ToLower().Contains(name.ToLower()))
        {
            result.Add(hero);
        }
    }
    return Ok(result);
    }

    [HttpGet("demo")]
    public ActionResult GetDemo() {
        var hero = HeroesStore.Heroes.First();
        var defaultOptions = new JsonSerializerOptions {
            WriteIndented = true
        };
        var ourOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter()}
        };
        return Ok(new {
            withDefaultSettings = JsonSerializer.Deserialize<object>(
                JsonSerializer.Serialize(hero, defaultOptions), defaultOptions),
            withOurSettings = JsonSerializer.Deserialize<object>( JsonSerializer.Serialize(hero, ourOptions), ourOptions),
        });
    }
    [HttpGet("serialize")]
    public ActionResult GetSerialize() {
        var options = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter()}
        };
        var hero = new Hero {
            Id =99,
            Name = "Тест",
            RealName = "Студент",
            Universe = Universe.Marvel,
            PowerLevel = 50,
            Powers = new() { "пролграммирование", "дебаггинг"},
            Weapon = new() {Name = "Клавиатура", IsRanged = false},
            InternalNotes = "Это поле не опдает в Json"
        };
        string serialized = JsonSerializer.Serialize(hero, options);
        var deserialized = JsonSerializer.Deserialize<Hero>(serialized, options);
        return Ok(new {
            serializedJson = serialized,
            deserializedObject=deserialized,
            internalNotesAfterDesetialize = deserialized?.InternalNotes ?? "null - поле было проигнорировано"
        });
    }
}