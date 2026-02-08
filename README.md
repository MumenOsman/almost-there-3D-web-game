#  Almost There

Hop into a cozy 3D parkour adventure! Play as a curious rabbit exploring a familiar, oversized world. Leap across platforms, discover hidden paths, and collect carrots at your own pace. Your unique playstyle is observed by an adaptive AI that gently shapes the challenges ahead, creating a personalized and comforting journey just for you.

## Gameplay
- Parkour-based movement (jump, dash, bounce)
- Collect carrots to progress through cozy micro-missions
- AI-adaptive level design that learns your skill and adjusts carrot placement
- No fail states, designed for flow and relaxation

## AI Usage
AI optimizes level design in real-time, not as enemies or procedural spam.
- Learns your skill level through movement and completion patterns
- Adjusts carrot placement and route difficulty dynamically
- Ensures the right challenge level without compromising cozy vibes

## Art Direction
- Soft, toy-like proportions
- Pastel colors and warm lighting

![DALL·E 2026-02-08 14 14 00 - A cozy 3D game scene rendered in warm, soft lighting with the same pastel, clean color style as before  In the center, a curious, fluffy brown cartoon](https://github.com/user-attachments/assets/2af62f76-e5e8-4131-999d-49b2e64ccaa8)

  

AI-generated art was used for **concept and reference only**.

## Tech Stack
- Unity (3D)
- AI: Gemini, Claude and DALL·E for game art
- Tools: Google Antigravity
- Target: Hackathon prototype

## Demo
👉 [Watch Gameplay Demo](DEMO_LINK_HERE) — Insert your YouTube/Drive link here

## 📋 Submission Documents
For detailed information, see:
- **[SETUP.md](SETUP.md)** — How to build and run the game
- **[IMPLEMENTATION.md](IMPLEMENTATION.md)** — AI technical architecture & design
- **[DOCS/](Docs/)** — Game design, AI design, art sources, story

## Technical Details

### Unity Version
- **Unity 6.2** (or later)
- Built as Windows executable (.exe)
- No external API keys required for base gameplay

### AI Integration
AI is integrated as a **level design system**, not a narrative mechanic:
- Tracks player skill through jump accuracy, speed, and carrot collection efficiency
- Adjusts carrot placement and route difficulty based on performance
- Dynamically optimizes level flow to maintain engagement
- Learns playstyle (cautious vs. risk-taking) and adapts accordingly

See [IMPLEMENTATION.md](IMPLEMENTATION.md) for full technical architecture.

### Performance
- Runs smoothly on modern hardware (60+ FPS)
- Minimal memory overhead (<50MB)
- Async AI response handling (no gameplay blocking)


## Team & Credits
- **Development:** Mumen, Maab
- **Art & Design:** Mumen, Maab
- **Tools Used:**
  - Unity 6.2 (game engine)
  - Claude (level design optimization & concept development)
  - DALL-E (concept art & environmental inspiration)


## How to Get Started

**Option 1: Play the Executable**
1. Download the `.exe` build from [RELEASE_LINK]
2. Run it directly—no installation needed
3. Enjoy the game!

**Option 2: Play from Source (Requires Unity 6.2)**
1. Clone this repository
2. Open in Unity 6.2
3. Load the main scene
4. Press Play

For detailed setup, see [SETUP.md](SETUP.md).

## 📁 Project Structure
```
almost-there-3D-web-game/
├── Assets/
│   ├── Scenes/        # Game scenes
│   ├── Scripts/       # C# gameplay & AI logic
│   ├── Models/        # 3D models (rabbit, kitchen, carrots)
│   ├── Animations/    # Character and UI animations
│   ├── UI/            # UI prefabs (dialogue, HUD)
│   └── Prompts/       # AI system prompts
├── Docs/              # Design documentation
│   ├── AI_DESIGN.md
│   ├── GAME_DESIGN.md
│   ├── Story.md
│   └── ART_SOURCES.md
├── SETUP.md           # Installation & build guide
├── IMPLEMENTATION.md  # AI technical deep-dive
└── README.md          # This file
```

## Contact & Feedback
We'd love to hear your thoughts! Feedback on the AI's tone, gameplay flow, or cozy aesthetics is welcome.
contact at: https://www.linkedin.com/in/mumenosman/

---

**Made with ❤️ for SuperCell AI Hackathon — 2026**

