# AI Design - Adaptive Level System (Planned Concept)

## Role of AI
The AI is designed as an **invisible, supportive difficulty balancer**.  
It would learn player skill and adjust the environment dynamically to keep the experience engaging and cozy.

## Why AI?
- Personalizes challenge without showing difficulty sliders
- Adapts to playstyle (cautious vs. risk-taking, fast vs. exploratory)
- Keeps gameplay engaging while respecting the cozy game feel
- Demonstrates intelligent, player-centered game design

## Planned AI Inputs
- Jump accuracy and landing precision
- Collection speed (how quickly carrots are collected)
- Route efficiency (direct path vs. exploratory)
- Pausing / hesitation behavior
- Retry frequency on sections

## Planned AI Decision Making
- Carrot Placement: easier for beginners, more challenging for skilled players
- Route Difficulty: open or gate paths according to skill
- Spacing: adjust distances between platforms or objects
- Timing: subtle adjustments in moving objects or platform layout

## Planned AI Outputs
- Modified level state (carrot positions, available routes)
- Difficulty progression tracking
- Invisible adjustments — no UI or dialogue notifications

## Safety & Design Philosophy
- Gradual, organic difficulty adaptation
- Cannot make the game unwinnable
- Maintains fun, flow, and cozy aesthetic at all times

> ⚠ Disclaimer: This AI system was **planned but not implemented** for the hackathon due to time constraints. The core gameplay loop (parkour + carrot collecting) is fully playable without it.

