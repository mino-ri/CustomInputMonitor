using System;
using System.Runtime.InteropServices;

namespace CustomInputMonitor;

public static class WinApi
{
    [DllImport("user32.dll")]
    public static extern bool GetKeyboardState(byte[] lpKeyState);

    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(VirtualKeyCode vKey);
}

public static class Message
{
    public const int KeyDown = 0x100;
    public const int SystemKeyDown = 0x104;
    public const int LButtonDown = 0x201;
    public const int RButtonDown = 0x204;
    public const int MButtonDown = 0x207;
    public const int XButtonDown = 0x20B;
}

public enum VirtualKeyCode
{
    None = 0x00,
    LButton = 0x01,
    RButton = 0x02,
    Cancel = 0x03,
    MButton = 0x04,
    XButton1 = 0x05,
    XButton2 = 0x06,
    Back = 0x08,
    Tab = 0x09,
    Clear = 0x0C,
    Return = 0x0D,
    Shift = 0x10,
    Control = 0x11,
    Alt = 0x12,
    Pause = 0x13,
    Capital = 0x14,
    Kana = 0x15,
    Hangul = 0x15,
    ImeOn = 0x16,
    Junja = 0x17,
    Final = 0x18,
    Hanja = 0x19,
    Kanji = 0x19,
    ImeOff = 0x1A,
    Escape = 0x1B,
    Convert = 0x1C,
    NonConvert = 0x1D,
    Accept = 0x1E,
    ModeChange = 0x1F,
    Space = 0x20,
    PageUp = 0x21,
    PageDown = 0x22,
    End = 0x23,
    Home = 0x24,
    Left = 0x25,
    Up = 0x26,
    Right = 0x27,
    Down = 0x28,
    Select = 0x29,
    Print = 0x2A,
    Execute = 0x2B,
    Snapshot = 0x2C,
    Insert = 0x2D,
    Delete = 0x2E,
    Help = 0x2F,
    D0 = 0x30,
    D1 = 0x31,
    D2 = 0x32,
    D3 = 0x33,
    D4 = 0x34,
    D5 = 0x35,
    D6 = 0x36,
    D7 = 0x37,
    D8 = 0x38,
    D9 = 0x39,
    A = 0x41,
    B = 0x42,
    C = 0x43,
    D = 0x44,
    E = 0x45,
    F = 0x46,
    G = 0x47,
    H = 0x48,
    I = 0x49,
    J = 0x4A,
    K = 0x4B,
    L = 0x4C,
    M = 0x4D,
    N = 0x4E,
    O = 0x4F,
    P = 0x50,
    Q = 0x51,
    R = 0x52,
    S = 0x53,
    T = 0x54,
    U = 0x55,
    V = 0x56,
    W = 0x57,
    X = 0x58,
    Y = 0x59,
    Z = 0x5A,
    LWin = 0x5B,
    RWin = 0x5C,
    Apps = 0x5D,
    Power = 0x5E,
    Sleep = 0x5F,
    Numpad0 = 0x60,
    Numpad1 = 0x61,
    Numpad2 = 0x62,
    Numpad3 = 0x63,
    Numpad4 = 0x64,
    Numpad5 = 0x65,
    Numpad6 = 0x66,
    Numpad7 = 0x67,
    Numpad8 = 0x68,
    Numpad9 = 0x69,
    Multiply = 0x6A,
    Add = 0x6B,
    Separator = 0x6C,
    Subtract = 0x6D,
    Decimal = 0x6E,
    Divide = 0x6F,
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,
    F8 = 0x77,
    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,
    F13 = 0x7C,
    F14 = 0x7D,
    F15 = 0x7E,
    F16 = 0x7F,
    F17 = 0x80,
    F18 = 0x81,
    F19 = 0x82,
    F20 = 0x83,
    F21 = 0x84,
    F22 = 0x85,
    F23 = 0x86,
    F24 = 0x87,
    NavigationView = 0x88,
    NavigationMenu = 0x89,
    NavigationUp = 0x8A,
    NavigationDown = 0x8B,
    NavigationLeft = 0x8C,
    NavigationRight = 0x8D,
    NavigationAccept = 0x8E,
    NavigationCancel = 0x8F,
    Numlock = 0x90,
    Scroll = 0x91,
    OemNecEqual = 0x92,
    OemFjJisho = 0x92,
    OemFjMasshou = 0x93,
    OemFjTouroku = 0x94,
    OemFjLOya = 0x95,
    OemFjROya = 0x96,
    LShift = 0xA0,
    RShift = 0xA1,
    LControl = 0xA2,
    RControl = 0xA3,
    LAlt = 0xA4,
    RAlt = 0xA5,
    BrowserBack = 0xA6,
    BrowserForward = 0xA7,
    BrowserRefresh = 0xA8,
    BrowserStop = 0xA9,
    BrowserSearch = 0xAA,
    BrowserFavorites = 0xAB,
    BrowserHome = 0xAC,
    VolumeMute = 0xAD,
    VolumeDown = 0xAE,
    VolumeUp = 0xAF,
    MediaNextTrack = 0xB0,
    MediaPrevTrack = 0xB1,
    MediaStop = 0xB2,
    MediaPlayPause = 0xB3,
    LaunchMail = 0xB4,
    LaunchMediaSelect = 0xB5,
    LaunchApp1 = 0xB6,
    LaunchApp2 = 0xB7,
    Oem1 = 0xBA,
    OemPlus = 0xBB,
    OemComma = 0xBC,
    OemMinus = 0xBD,
    OemPeriod = 0xBE,
    Oem2 = 0xBF,
    Oem3 = 0xC0,
    AbntC1 = 0xC1,
    AbntC2 = 0xC2,
    GamepadA = 0xC3,
    GamepadB = 0xC4,
    GamepadX = 0xC5,
    GamepadY = 0xC6,
    GamepadRightShoulder = 0xC7,
    GamepadLeftShoulder = 0xC8,
    GamepadLeftTrigger = 0xC9,
    GamepadRightTrigger = 0xCA,
    GamepadDPadUp = 0xCB,
    GamepadDPadDown = 0xCC,
    GamepadDPadLeft = 0xCD,
    GamepadDPadRight = 0xCE,
    GamepadMenu = 0xCF,
    GamepadView = 0xD0,
    GamepadLeftThumbStickButton = 0xD1,
    GamepadRightThumbStickButton = 0xD2,
    GamepadLeftThumbStickUp = 0xD3,
    GamepadLeftThumbStickDown = 0xD4,
    GamepadLeftThumbStickRight = 0xD5,
    GamepadLeftThumbStickLeft = 0xD6,
    GamepadRightThumbStickUp = 0xD7,
    GamepadRightThumbStickDown = 0xD8,
    GamepadRightThumbStickRight = 0xD9,
    GamepadRightThumbStickLeft = 0xDA,
    Oem4 = 0xDB,
    Oem5 = 0xDC,
    Oem6 = 0xDD,
    Oem7 = 0xDE,
    Oem8 = 0xDF,
    OemAx = 0xE1,
    Oem102 = 0xE2,
    IcoHelp = 0xE3,
    Ico00 = 0xE4,
    ProcessKey = 0xE5,
    IcoClear = 0xE6,
    Packet = 0xE7,
    OemReset = 0xE9,
    OemJump = 0xEA,
    OemPa1 = 0xEB,
    OemPa2 = 0xEC,
    OemPa3 = 0xED,
    OemWsCtrl = 0xEE,
    OemCusel = 0xEF,
    OemAttn = 0xF0,
    OemFinish = 0xF1,
    OemCopy = 0xF2,
    OemAuto = 0xF3,
    OemEnlw = 0xF4,
    OemBackTab = 0xF5,
    Attn = 0xF6,
    Crsel = 0xF7,
    Exsel = 0xF8,
    Ereof = 0xF9,
    Play = 0xFA,
    Zoom = 0xFB,
    Noname = 0xFC,
    Pa1 = 0xFD,
    OemClear = 0xFE,
}
