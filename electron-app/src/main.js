const { app, BrowserWindow } = require('electron');
const { spawn } = require('child_process');
const path = require('path');

let angularProcess, backendProcess;

function createWindow() {
  const win = new BrowserWindow({
    width: 1200,
    height: 800,
    webPreferences: {
      contextIsolation: true,
    },
  });

  // Wait for Angular to start (improve this later with polling if needed)
  setTimeout(() => {
    win.loadURL('http://localhost:4200');
  }, 10000);
}

app.whenReady().then(() => {
  // Ensure cmd.exe path exists in PATH (optional but safe)
  const env = {
    ...process.env,
    PATH: `C:\\Windows\\System32;C:\\Windows;C:\\Program Files\\nodejs;${process.env.PATH || ''}`
  };

  // 🟢 Start Angular
  angularProcess = spawn('npm', ['start'], {
    cwd: path.join(__dirname, '../angular-ui'),
    shell: true,
    stdio: 'inherit',
    env
  });

  // 🟢 Start ASP.NET Core using full path to .csproj
  backendProcess = spawn('dotnet', ['run', '--project', './src/API/API.csproj'], {
    cwd: path.join(__dirname, '..'),
    shell: true,
    stdio: 'inherit',
    env
  });

  // 🟢 Launch Electron Window
  createWindow();
});

// 🧹 Kill processes when Electron closes
app.on('will-quit', () => {
  if (angularProcess) angularProcess.kill();
  if (backendProcess) backendProcess.kill();
});
