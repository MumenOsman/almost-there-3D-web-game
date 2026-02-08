# 🤖 AI Implementation Details

## Architecture Overview

```
Player Input → Game Logic → AI Service → Response → UI Update
```

### Flow Diagram
1. **Player Actions** (jump, move, collect) → Game Event System
2. **Game State** (position, carrot count, time idle) → AI Input Processing
3. **AI Service** (Claude API or local response system) → Generate dialogue
4. **Dialogue System** → Display pocket watch reaction
5. **Loop continues** based on player behavior

## Core Components

### 1. AI Companion System
**File:** `Assets/Scripts/AI/CompanionAI.cs` (example structure)

```csharp
public class CompanionAI : MonoBehaviour
{
    // Tracks player behavior
    private PlayerBehavior playerStats;
    
    // Sends input to AI service
    public async Task<string> GetCompanionResponse(GameState state)
    {
        var prompt = BuildPrompt(state);
        var response = await AIService.GetResponse(prompt);
        return response;
    }
}
```

### 2. Input Generation
The AI ingests real-time player data:

| Input | Source | Example |
|-------|--------|---------|
| **Movement Pattern** | Input tracking | "Player keeps falling off the same platform" |
| **Carrot Collection Rate** | Game logic | "Collected 5 carrots in 2 minutes" |
| **Idle Time** | Frame counter | "Standing still for 30 seconds" |
| **Risk-Taking** | Physics detection | "Attempted a difficult jump 3 times" |
| **Hesitation** | Movement speed analysis | "Moving slowly, taking careful steps" |

### 3. AI Response Generation
The system creates contextual dialogue:

**System Prompt** (from `Assets/Prompts/AI_COMPANION_PROMPT.txt`):
```
You are a gentle, supportive pocket watch companion...
```

**Input Prompt Example:**
```
The player just fell from the same platform for the 3rd time.
They're still trying. How do you gently encourage them?
Keep it to one short sentence.
```

**Output Example:**
```
"You've got this! That jump takes a moment to master. 💛"
```

### 4. Dialogue System
**File:** `Assets/Scripts/UI/DialogueManager.cs` (example structure)

```csharp
public class DialogueManager : MonoBehaviour
{
    private TextMeshProUGUI dialogueBox;
    
    public void DisplayResponse(string text)
    {
        StartCoroutine(TypeWriter(text, duration: 3f));
        // Auto-hide after 3 seconds
    }
}
```

**Output:** Text appears above/near the pocket watch UI element

### 5. Integration Points

| System | Integration | Notes |
|--------|-----------|-------|
| **Player Movement** | OnJump, OnLand events | Detects patterns |
| **Carrot Collection** | Inventory system | Tracks progress |
| **Level Progress** | Checkpoint system | Celebrates milestones |
| **Camera** | Visual system | Pocket watch follows player |

## AI Decision Making

### What Triggers a Response?

**Every 5-10 seconds OR when:**
- Player completes a carrot collection
- Player falls (without losing)
- Player hesitates for >15 seconds
- Player successfully completes a difficult jump
- First time entering a new area

### Response Selection
- **Encouragement** (40%) - "You're doing great!"
- **Observation** (30%) - "I see you're trying that again"
- **Celebration** (20%) - "Nice collection! 🥕"
- **Gentle nudge** (10%) - "The next carrot is just ahead"

## No Fail States
AI **never** delivers:
- ❌ Criticism or judgment
- ❌ Pressure or urgency ("Hurry!")
- ❌ Repetitive comments about failures
- ❌ Generic/spam responses

## API Usage (Demo Flow)

### During Development
- Uses Claude API (in demo/showcase mode)
- Cost: ~$0.01 per 10 responses (minimal)

### In Hackathon Build
- Responses pre-generated and cached
- OR uses lightweight local response system
- **Zero API calls** needed during normal play

## Performance Considerations

| Metric | Target | Status |
|--------|--------|--------|
| Response latency | <500ms | ✅ Cached responses |
| Memory overhead | <50MB | ✅ Small prompt + response |
| CPU impact | <2% | ✅ Async processing |

## Safety & Ethics

### Data Handling
- ✅ No player data storage
- ✅ No behavioral tracking
- ✅ No profiling
- ✅ Session-only memory

### Bias Mitigation
- Responses tested for neutrality
- No gendered language
- Accessible language (not overly complex)

### Content Moderation
- AI prompt includes safety guidelines
- Responses filtered before display
- Fallback strings for any edge cases

## Testing the AI

### Manual Testing Checklist
- [ ] AI responds when player jumps
- [ ] AI adapts to different playstyles
- [ ] Dialogue appears and disappears correctly
- [ ] No profanity or negative language
- [ ] Responses feel natural in context
- [ ] Performance remains smooth

### Example Scenarios
1. **Fast player**: "You're moving with confidence!"
2. **Struggling player**: "No rush, this one's tricky!"
3. **Extended pause**: "Whenever you're ready! 💙"
4. **Success milestone**: "You've collected 10 carrots! Amazing! 🎉"

## Future Enhancements

- [ ] Dynamic difficulty conversation
- [ ] Player name integration
- [ ] Mood/tone customization
- [ ] Multi-language support
- [ ] Learning from player preferences
