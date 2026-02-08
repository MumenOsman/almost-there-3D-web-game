# 🛠 Setup & Build Instructions

## Prerequisites
- **Unity 6.2** or later
- Windows, macOS, or Linux

## Installation

### From Source
1. Clone the repository:
   ```bash
   git clone [YOUR_REPO_URL]
   cd SuperCellHack2
   ```

2. Open the project in Unity 6.2:
   - Launch Unity Hub
   - Click "Open Project"
   - Select the `SuperCellHack2` folder
   - Wait for Unity to import assets (this may take 1-2 minutes)

3. Run the game:
   - Open the main scene from `Assets/Scenes/Main.unity`
   - Press **Play** in the Unity Editor

### Building an Executable

#### Windows (.exe)
1. Go to **File > Build Settings**
2. Select **PC, Mac & Linux Standalone**
3. Set Target Platform to **Windows**
4. Click **Build**
5. Choose output folder (e.g., `Builds/Windows`)
6. Run `SuperCellHack2.exe` to play

#### macOS (.app)
1. Go to **File > Build Settings**
2. Select **PC, Mac & Linux Standalone**
3. Set Target Platform to **Mac**
4. Click **Build**
5. Choose output folder
6. Run the generated `.app` file

## Game Controls
- **WASD** - Move (or Arrow Keys)
- **Space** - Jump
- **Shift** - Dash
- **Mouse** - Look around
- **ESC** - Pause menu

## Troubleshooting

**Unity won't load the project?**
- Make sure you're using Unity 6.2+
- Delete the `Library` folder and reopen the project

**Graphics issues?**
- Check your GPU drivers are up to date
- Reduce quality settings in game options

**AI responses not showing?**
- Ensure internet connection (AI dialogue uses cloud API only in demo mode)
- Check console for error messages (Window > General > Console)

## Performance
- Recommended: GPU with 2GB+ VRAM, 4GB+ RAM
- Minimum: Integrated GPU, 4GB RAM
- Typical FPS: 60+ on modern hardware

## No External Dependencies
This project requires **no API keys** for base gameplay. The AI companion works with pre-configured responses for the main game loop.
