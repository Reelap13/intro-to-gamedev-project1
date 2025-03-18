using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Setting
{
    public static Settings.Audio Audio { get; } = new Settings.Audio();
    public static Settings.Quality Quality { get; } = new Settings.Quality();
    public static Settings.ResolutionScreenAndFullSceen Screen { get; } = new Settings.ResolutionScreenAndFullSceen();
    public static Settings.Difficulty Difficulty { get; } = new Settings.Difficulty();
}
