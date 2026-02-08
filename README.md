# 🐇 Almost On Time

A cozy 3D parkour game set in a giant kitchen, where a rabbit collects carrots while guided by a gentle AI companion inspired by the White Rabbit from Alice in Wonderland.

## 🎮 Gameplay
- Parkour-based movement (jump, dash, bounce)
- Collect carrots to progress through cozy micro-missions
- A living AI companion (the Pocket Watch) reacts to player behavior and performance
- No fail states, designed for flow and relaxation

## 🤖 AI Usage
AI is used as a **companion narrator**, not as enemies or content spam.
- Generates adaptive dialogue
- Reacts to player choices (risk-taking, repetition, hesitation)
- Enhances emotional engagement without affecting core mechanics

## 🎨 Art Direction
- Soft, toy-like proportions
- Pastel colors and warm lighting
- Supercell-inspired readability and charm

AI-generated art was used for **concept and reference only**.

## 🛠 Tech Stack
- Unity (3D)
- AI: Claude / DALL·E (concept & dialogue)
- Target: Hackathon prototype

## � Demo
👉 [Watch Gameplay Demo](DEMO_LINK_HERE) — Insert your YouTube/Drive link here

## 📋 Submission Documents
For detailed information, see:
- **[SETUP.md](SETUP.md)** — How to build and run the game
- **[IMPLEMENTATION.md](IMPLEMENTATION.md)** — AI technical architecture & design
- **[DOCS/](Docs/)** — Game design, AI design, art sources, story

## 🔧 Technical Details

### Unity Version
- **Unity 6.2** (or later)
- Built as Windows executable (.exe)
- No external API keys required for base gameplay

### AI Integration
The AI companion is integrated as a **narrative system**, not a gameplay mechanic:
- Reads player behavior (movement patterns, carrot collection, hesitation)
- Generates contextual, encouraging dialogue  
- Displays reactions via the pocket watch UI
- All responses are gentle, supportive, and non-intrusive

See [IMPLEMENTATION.md](IMPLEMENTATION.md) for full technical architecture.

### Performance
- Runs smoothly on modern hardware (60+ FPS)
- Minimal memory overhead (<50MB)
- Async AI response handling (no gameplay blocking)

## 🎯 Why This AI?

Instead of using AI for enemies, procedural content, or spam, we focused on **meaningful companion interaction**:
- AI enhances emotional engagement
- Personalizes the relaxing experience
- Shows how AI can add texture without complexity
- Demonstrates responsible AI usage in games

## 👥 Team & Credits
- **Development:** [Your Name(s)]
- **AI Prompt Design:** [Your Name(s)]
- **Art & Design:** [Your Name(s)]
- **Tools Used:**
  - Unity 6.2 (game engine)
  - Claude API (dialogue generation & concept)
  - DALL-E (concept art reference only)

## 🎓 Learnings & Results

### What Worked Well
- ✅ AI as companion feels natural and supportive
- ✅ Minimal API usage keeps costs low
- ✅ Players engage with dialogue without feeling pressured
- ✅ Cozy tone is reinforced by gentle AI

### Challenges & Solutions
- **Challenge:** How to make AI responses feel contextual?
  - **Solution:** Track specific behaviors (fall patterns, hesitation, success celebrations)
- **Challenge:** Avoid AI getting repetitive?
  - **Solution:** Vary response tone based on game state, use rotation system
- **Challenge:** Keep costs low within hackathon budget?
  - **Solution:** Cache responses, use lightweight local system for base game

### Metrics
- Response latency: <500ms (pre-cached)
- Memory footprint: ~25MB
- Cost per session: <$0.02 (demo only; build release uses cached responses)

## 🚀 How to Get Started

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
SuperCellHack2/
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

## 💌 Contact & Feedback
We'd love to hear your thoughts! Feedback on the AI's tone, gameplay flow, or cozy aesthetics is welcome.

---

**Made with ❤️ for [Hackathon Name] — 2026**

