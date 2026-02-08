# AI Design - Adaptive Level System

## Role of AI
AI acts as an **invisible difficulty balancer** that learns player skill and adjusts level design dynamically.

## Why AI?
- Personalizes challenge level without obvious difficulty settings
- Learns playstyle (cautious vs. risk-taking, fast vs. exploratory)
- Ensures engagement by avoiding boredom and frustration
- Demonstrates intelligent game design vs. brute-force difficulty

## AI Inputs
- Jump accuracy and landing precision
- Collection speed (how quickly player collects carrots)
- Route efficiency (direct path vs. exploration)
- Hesitation duration (time spent pausing before action)
- Retry patterns (how many times player attempts a section)

## AI Decision Making
Based on inputs, AI adjusts:
- **Carrot Placement** - More accessible positions vs. challenging ones
- **Route Difficulty** - Open easier paths or gate harder ones
- **Spacing** - Spread carrots further apart for skilled players
- **Timing** - Adjust platform positions to match player skill

## AI Outputs
- Modified level state (carrot positions, available routes)
- Difficulty score tracking player progression
- No dialogue or UI notifications (invisible to player)

## Safety & Design
- No frustrating difficulty spikes
- Cannot make the game unwinnable
- Adjustments are gradual and organic
- Respects cozy game feel above all
