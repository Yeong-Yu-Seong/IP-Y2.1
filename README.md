# 🕵️‍♂️ Catch That Crook!

**Catch That Crook!** is a 3D educational game where you play as a cashier in a toy store. Your goal is to catch NPCs (non-player characters) who attempt to shoplift. Observe their behavior, make quick decisions, and catch them before they escape!

## 🎮 Game Overview

> *“Keep your eyes sharp. Some customers have sticky fingers.”*

You are the store's only line of defense against theft. Some NPCs will attempt to steal items as they browse. Your job is to stop them before they leave. Each encounter requires close observation — who’s just shopping, and who’s trying to sneak out unpaid?

The game raises awareness about shoplifting and encourages ethical behavior through fun and interactive gameplay.

## 🕹️ Controls

| Key | Action                        |
|-----|-------------------------------|
| `E` | Interact (Catch thief NPC)    |
| `Q` | Return to Main Menu           |

> ⚠️ Known Issues:
> - Interact UI is supposed to appear near NPCs when in range, but is **currently not showing**.
> - Pressing `Q` stops **NPC spawning**, but already active NPCs **continue moving**.

## 🧠 NPC AI & FSM Behavior

All NPCs in the game are **parent-child pairs**, representing a parent shopping with their child. Only the parent can steal.

### 👨‍👧 AI States (Parent NPC)
1. **Patrol Shelves**
   - NPC follows a predefined shelf path.
   - At each shelf, there's a **5% chance to steal**.
2. **Steal or Not Decision**
   - If **stealing**, NPC will:
     - Continue to last shelf.
     - Exit store without paying.
   - If **not stealing**, NPC will:
     - Proceed to the **cashier counter** and pay.
3. **Caught**
   - If the player presses `E` near a thief NPC before they escape:
     - **Both parent and child disappear**.
     - Thief is counted as **caught**.
4. **Escape**
   - If a thief exits undetected, they're counted as **not caught**.

### FSM Diagram (Text Representation)


## 📺 UI Screens

1. **Start Menu**
   - `Play`
   - `How to Play`

2. **How to Play**
   - Shows controls and game overview

3. **In-Game UI**
   - Controls reminder
   - Score counter (Thieves caught)

4. **End Game Screen**
   - Displays:
     - Thieves Caught
     - Thieves Escaped
   - `Play Again` button

## 🧩 Puzzle Answers

There are **no puzzles** in the current version of the game.

## 📦 Bugs & Limitations

- Interact UI not showing.
- Returning to menu (`Q`) stops NPC spawning but doesn't stop already spawned NPCs from walking.
- No animations or visual cues to clearly indicate stealing behavior (if applicable).
- NPC behavior is probabilistic and may sometimes lead to uneventful rounds.

## 🎨 Assets & Credits

| Asset                      | Type         | Source / Creator      |
|---------------------------|--------------|------------------------|
| Toy Store Environment     | 3D Models    | (Insert source here)   |
| Parent & Child NPC Models | 3D Models    | (Insert source here)   |
| UI Icons / Fonts          | UI Elements  | (Insert source here)   |
| Sound Effects / Music     | Audio        | (Insert source here)   |


---

## ✅ Final Notes

- Built in **Unity**
- Designed for **laptop/desktop gameplay**
- Created for **educational purposes** to teach vigilance and retail ethics