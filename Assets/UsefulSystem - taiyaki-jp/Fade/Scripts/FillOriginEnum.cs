namespace FadeOptions
{
    public enum Horizontal
    {
        Left = 0,
        Right,
    }

    public enum Vertical
    {
        Bottom = 0,
        Top,
    }

    public enum Radial_90
    {
        BottomLeft = 0,
        TopLeft,
        TopRight,
        BottomRight,
    }

    public enum Radial_180
    {
        Bottom = 0,
        Left,
        Top,
        Right,
    }

    public enum Radial_360
    {
        Bottom = 0,
        Left,
        Top,
        Right,
    }
}

public enum FadeActionMode
{
    BeforeFade,
    AfterFade,
    FinishFade
}

public enum FadeMode
{
    FadeIn = 1,
    FadeOut = -1,
}
