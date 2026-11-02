
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace TTS_BOT_2.TTS
{
    public class MixAudios
    {
        public byte[] mixAudio(List<string> syllablesPath)
        {

            List<AudioFileReader> readers = new();

            foreach (string path in syllablesPath)
            {
                if (File.Exists(path))
                {
                    readers.Add(new AudioFileReader(path));
                }
            }

            try
            {
                var concatenated = new ConcatenatingSampleProvider(readers);

                using var output = new MemoryStream();

                var waveProvider = new SampleToWaveProvider(concatenated);

                WaveFileWriter.WriteWavFileToStream(output, waveProvider);

                return output.ToArray();
            }
            finally
            {
                foreach (var reader in readers)
                {
                    reader.Dispose();
                }
            }
        }
    }
}
