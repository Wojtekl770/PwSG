#include <windows.h>
#include <tchar.h>
#include <stdio.h>
#include <cmath>
#include <vector>
#include <string>

#define ID_TIMER 1
#define WINDOW_ALPHA 20
#define PULSE_STEP 10
#define PULSE_FREQUENCY 0.5
#define WINDOW_RADIUS 100
#define MAX_KEYS_DISPLAYED 10
#define DISPLAY_DURATION 5000
#define TRAY_ICON_ID 1
#define WM_SYSTRAY (WM_USER + 1)
COLORREF chosenColor = RGB(255, 254, 0);

// Global variables
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
ATOM MyRegisterClass(HINSTANCE hInstance);
BOOL InitInstance(HINSTANCE, int);
void DrawMouseHighlight(HDC hdc, int x, int y, int radius);
void AddKeyPressed(const std::wstring& key);
void DrawNotification(HDC hdc);
void ShowHelp();
void ShowConfigFile();
void ReloadConfig();
void PickColor(HWND hWnd);
void AddTrayIcon(HWND hWnd);
void RemoveTrayIcon(HWND hWnd);
void ShowContextMenu(HWND hWnd);

HINSTANCE hInst;
HWND hWnd;
HDC hDC;
HDC hMemDC;
HBITMAP hBitmap;
RECT desktopRect;
POINT cursorPos;
double circleRadius = 50;
std::vector<std::wstring> keysPressed;
std::wstring lastKeyPressed;

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
    _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPWSTR lpCmdLine,
    _In_ int nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    MyRegisterClass(hInstance);

    if (!InitInstance(hInstance, nCmdShow))
    {
        return FALSE;
    }

    MSG msg;
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = LoadIcon(hInstance, IDI_APPLICATION);
    wcex.hCursor = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszMenuName = nullptr;
    wcex.lpszClassName = L"MouseHighlightWindow";
    wcex.hIconSm = LoadIcon(wcex.hInstance, IDI_APPLICATION);
    return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
    hInst = hInstance;

    GetClientRect(GetDesktopWindow(), &desktopRect);

    hDC = GetDC(NULL);
    hMemDC = CreateCompatibleDC(hDC);
    hBitmap = CreateCompatibleBitmap(hDC, desktopRect.right, desktopRect.bottom);
    SelectObject(hMemDC, hBitmap);

    hWnd = CreateWindowEx(WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW, L"MouseHighlightWindow", nullptr, WS_POPUP,
        0, 0, desktopRect.right, desktopRect.bottom, nullptr, nullptr, hInstance, nullptr);

    if (!hWnd)
    {
        return FALSE;
    }

    SetLayeredWindowAttributes(hWnd, RGB(0, 0, 0), WINDOW_ALPHA, LWA_COLORKEY | LWA_ALPHA);
    SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

    SetTimer(hWnd, ID_TIMER, PULSE_STEP, NULL);

    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);

    AddTrayIcon(hWnd);

    return TRUE;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_DESTROY:
        // Cleanup resources
        RemoveTrayIcon(hWnd);
        KillTimer(hWnd, ID_TIMER);
        DeleteDC(hMemDC);
        DeleteObject(hBitmap);
        ReleaseDC(NULL, hDC);
        PostQuitMessage(0);
        break;
    case WM_TIMER:
        if (wParam == ID_TIMER)
        {
            // Update window position to follow the mouse cursor
            GetCursorPos(&cursorPos);
            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
            circleRadius = 50 + (WINDOW_RADIUS - 50) * 0.25 * (1 + sin(2 * 3.14159265358979323846 * PULSE_FREQUENCY * GetTickCount() / 1000.0));
            InvalidateRect(hWnd, NULL, TRUE);
        }
        break;
    case WM_PAINT:
    {
        ShowHelp();
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hWnd, &ps);

        HDC hdcBuffer = CreateCompatibleDC(hdc);
        HBITMAP hBitmapBuffer = CreateCompatibleBitmap(hdc, desktopRect.right, desktopRect.bottom);
        SelectObject(hdcBuffer, hBitmapBuffer);

        DrawMouseHighlight(hdcBuffer, cursorPos.x - circleRadius, cursorPos.y - circleRadius, circleRadius);
        DrawNotification(hdcBuffer);

        BitBlt(hdc, 0, 0, desktopRect.right, desktopRect.bottom, hdcBuffer, 0, 0, SRCCOPY);

        DeleteObject(hBitmapBuffer);
        DeleteDC(hdcBuffer);

        EndPaint(hWnd, &ps);
    }
    break;
    case WM_LBUTTONDOWN:
        break;
    case WM_RBUTTONDOWN:
    {
        POINT pt;
        GetCursorPos(&pt);
        HMENU hPopupMenu = CreatePopupMenu();
        InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 1, L"Exit");
        InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 2, L"Open config file");
        InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 3, L"Reload config");
        InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 4, L"Pick color...");
        TrackPopupMenu(hPopupMenu, TPM_RIGHTBUTTON, pt.x, pt.y, 0, hWnd, NULL);
        DestroyMenu(hPopupMenu);
    }
    break;
    case WM_COMMAND:
        switch (LOWORD(wParam))
        {
        case 1:
            DestroyWindow(hWnd);
            break;
        case 2:
            ShowConfigFile();
            break;
        case 3:
            ReloadConfig();
            break;
        case 4:
            PickColor(hWnd);
            break;
        default:
            break;
        }
        break;
    case WM_CREATE:
        AddTrayIcon(hWnd);
        break;
    case WM_SYSTRAY:
        switch (lParam)
        {
        case WM_RBUTTONDOWN:
        case WM_CONTEXTMENU:
            ShowContextMenu(hWnd);
            break;
        default:
            break;
        }
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

void DrawMouseHighlight(HDC hdc, int x, int y, int radius)
{
    double time = GetTickCount() / 1000.0;
    double pulseFactor = 0.25 * (1 + sin(2 * 3.14159265358979323846 * PULSE_FREQUENCY * time));
    int pulseRadius = radius + (int)((WINDOW_RADIUS - radius) * pulseFactor);

    int topLeftX = x;
    int topLeftY = y;

    HBRUSH hBrush = CreateSolidBrush(chosenColor);
    HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);
    int oldBkMode = SetBkMode(hdc, TRANSPARENT);
    Ellipse(hdc, topLeftX, topLeftY, topLeftX + 2 * pulseRadius, topLeftY + 2 * pulseRadius);
    SetBkMode(hdc, oldBkMode);
    SelectObject(hdc, hOldBrush);
    DeleteObject(hBrush);
}

void AddKeyPressed(const std::wstring& key)
{
    keysPressed.push_back(key);
    if (keysPressed.size() > MAX_KEYS_DISPLAYED)
    {
        keysPressed.erase(keysPressed.begin());
    }
    lastKeyPressed = key;
    InvalidateRect(hWnd, NULL, TRUE);
}

void DrawNotification(HDC hdc)
{
    if (!lastKeyPressed.empty())
    {
        RECT textRect;
        GetClientRect(hWnd, &textRect);
        textRect.bottom -= 20;
        textRect.left = desktopRect.right - 150;
        textRect.top = desktopRect.bottom - 20;

        SetTextColor(hdc, RGB(255, 255, 255));
        SetBkColor(hdc, RGB(0, 0, 0));
        DrawText(hdc, lastKeyPressed.c_str(), -1, &textRect, DT_SINGLELINE | DT_RIGHT | DT_BOTTOM);
    }
}

void ShowHelp()
{
    MessageBox(NULL, L"Help message here...", L"Help", MB_OK | MB_ICONINFORMATION);
}

void ShowConfigFile()
{
    TCHAR szFileName[MAX_PATH];
    GetModuleFileName(NULL, szFileName, MAX_PATH);
    std::wstring exePath = szFileName;
    std::wstring::size_type pos = exePath.find_last_of(L"\\/");
    if (pos != std::wstring::npos)
    {
        exePath = exePath.substr(0, pos);
    }
    std::wstring iniFile = exePath + L"\\config.ini";
    ShellExecute(NULL, L"open", iniFile.c_str(), NULL, NULL, SW_SHOWNORMAL);
}

void ReloadConfig()
{
    TCHAR szFileName[MAX_PATH];
    GetModuleFileName(NULL, szFileName, MAX_PATH);
    std::wstring exePath = szFileName;
    std::wstring::size_type pos = exePath.find_last_of(L"\\/");
    if (pos != std::wstring::npos)
    {
        exePath = exePath.substr(0, pos);
    }
    std::wstring iniFile = exePath + L"\\config.ini";

    TCHAR szBuffer[1024];
    GetPrivateProfileString(L"Settings", L"Key1", L"", szBuffer, 1024, iniFile.c_str());
}

void PickColor(HWND hWnd)
{
    CHOOSECOLOR cc;
    static COLORREF acrCustClr[16];

    ZeroMemory(&cc, sizeof(cc));
    cc.lStructSize = sizeof(cc);
    cc.hwndOwner = hWnd;
    cc.lpCustColors = (LPDWORD)acrCustClr;
    cc.rgbResult = chosenColor;
    cc.Flags = CC_FULLOPEN | CC_RGBINIT;

    if (ChooseColor(&cc) == TRUE)
    {
        chosenColor = cc.rgbResult;
        InvalidateRect(hWnd, NULL, TRUE);
    }
}

void AddTrayIcon(HWND hWnd)
{
    NOTIFYICONDATA nid;
    ZeroMemory(&nid, sizeof(NOTIFYICONDATA));
    nid.cbSize = sizeof(NOTIFYICONDATA);
    nid.hWnd = hWnd;
    nid.uID = TRAY_ICON_ID;
    nid.uFlags = NIF_ICON | NIF_MESSAGE | NIF_TIP;
    nid.uCallbackMessage = WM_SYSTRAY;
    nid.hIcon = LoadIcon(hInst, IDI_APPLICATION);
    _tcscpy_s(nid.szTip, _countof(nid.szTip), _T("Mouse Highlighter"));
    Shell_NotifyIcon(NIM_ADD, &nid);
}

void RemoveTrayIcon(HWND hWnd)
{
    NOTIFYICONDATA nid;
    ZeroMemory(&nid, sizeof(NOTIFYICONDATA));
    nid.cbSize = sizeof(NOTIFYICONDATA);
    nid.hWnd = hWnd;
    nid.uID = TRAY_ICON_ID;
    Shell_NotifyIcon(NIM_DELETE, &nid);
}

void ShowContextMenu(HWND hWnd)
{
    POINT pt;
    GetCursorPos(&pt);
    HMENU hPopupMenu = CreatePopupMenu();
    InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 1, L"Exit");
    InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 2, L"Open config file");
    InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 3, L"Reload config");
    InsertMenu(hPopupMenu, -1, MF_BYPOSITION | MF_STRING, 4, L"Pick color...");
    TrackPopupMenu(hPopupMenu, TPM_RIGHTBUTTON, pt.x, pt.y, 0, hWnd, NULL);
    DestroyMenu(hPopupMenu);
}
