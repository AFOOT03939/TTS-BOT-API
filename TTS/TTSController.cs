using Microsoft.AspNetCore.Mvc;
using TTS_BOT.Services;
using TTS_BOT_2.TTS;

namespace TTS_BOT_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TTSController : ControllerBase
    {

        private SyllabificationService _syllabificationService;
        private MixAudios _mixAudios;

        public TTSController(SyllabificationService syllabificationService, MixAudios mixAudios) { 
            
            _syllabificationService = syllabificationService;
            _mixAudios = mixAudios;
        }

        [HttpGet]
        public IActionResult getSyllables([FromQuery] string text)
        {
            List<string> paths = new List<string>();

            List<string> syllables = _syllabificationService.textReciever(text);

            foreach (var syllable in syllables)
            {
                paths.Add(Path.Combine("E:\\Users\\victo\\source\\repos\\TTS BOT 2\\TTS BOT 2\\Audios\\" + syllable + ".wav"));
            }

            var audioWav = _mixAudios.mixAudio(paths);

            return File(audioWav, "audio/wav");
        }
    }
}
