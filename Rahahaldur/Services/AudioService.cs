using Plugin.Maui.Audio;

namespace RahaHaldur.Services;

// Heliefektid ja taustamuusika. Kui heli on seadetes välja lülitatud, ei mängi midagi.
public class AudioService
{
    private readonly IAudioManager _audioManager;
    private readonly SettingsService _settings;
    private IAudioPlayer _music;

    public AudioService(IAudioManager audioManager, SettingsService settings)
    {
        _audioManager = audioManager;
        _settings = settings;
    }

    // Lühike heliefekt, nt "coin.wav" (fail on kaustas Resources/Raw)
    public async Task PlayEffectAsync(string fileName)
    {
        if (!_settings.SoundOn)
            return;

        try
        {
            var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            var player = _audioManager.CreatePlayer(stream);
            player.Play();
        }
        catch (Exception ex)
        {
            // Heli viga ei tohi rakendust kinni panna
            System.Diagnostics.Debug.WriteLine("Heli viga: " + ex.Message);
        }
    }

    // Käivitab taustamuusika (kordub lõputult)
    public async Task StartMusicAsync()
    {
        if (!_settings.SoundOn)
            return;

        try
        {
            if (_music == null)
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("music.wav");
                _music = _audioManager.CreatePlayer(stream);
                _music.Loop = true;
                _music.Volume = 0.4;
            }

            _music.Play();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Muusika viga: " + ex.Message);
        }
    }

    // Peatab muusika (Play jätkab samast kohast)
    public void StopMusic()
    {
        if (_music != null)
            _music.Pause();
    }
}
