using System.Runtime.InteropServices;
using Win32Api;

using static Win32Api.NativeMethods;

// 윈도우 클래스 등록
StructureFactory.InitializeWithSize(out WNDCLASSEX wndClassEx, (ref WNDCLASSEX o) =>
{
    o.lpfnWndProc = new WndProc(WndProc);
    o.hIcon = LoadIconW(IntPtr.Zero, IDI_APPLICATION);
    o.hCursor = LoadCursorW(IntPtr.Zero, IDC_ARROW);
    o.hbrBackground = 1 + 1;
    o.lpszClassName = "HelloWorldClass";
    o.hIconSm = LoadIconW(IntPtr.Zero, IDI_APPLICATION);
});

RegisterClassExW(ref wndClassEx);

// 윈도우 생성
IntPtr hWnd = CreateWindowExW(
    0,
    "HelloWorldClass",
    "Hello, World Program",
    WS_OVERLAPPEDWINDOW,
    CW_USEDEFAULT, CW_USEDEFAULT,
    500, 100,
    IntPtr.Zero,
    IntPtr.Zero,
    IntPtr.Zero,
    IntPtr.Zero);

ShowWindow(hWnd, SW_SHOWNORMAL);
UpdateWindow(hWnd);

while (GetMessageW(out MSG msg, IntPtr.Zero, 0, 0))
{
    TranslateMessage(ref msg);
    DispatchMessageW(ref msg);
}

static IntPtr WndProc(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int msg, IntPtr wParam, IntPtr lParam)
{
    switch (msg)
    {
        case WM_PAINT:
            PAINTSTRUCT ps;
            var hdc = BeginPaint(hWnd, out ps);
            var message = "안녕하세요!";
            TextOutW(hdc, 5, 5, message, message.Length);
            EndPaint(hWnd, ref ps);
            break;

        case WM_DESTROY:
            PostQuitMessage(0);
            break;

        default:
            return DefWindowProcW(hWnd, msg, wParam, lParam);
    }
    return IntPtr.Zero;
}
