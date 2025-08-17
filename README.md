# 🕵️‍♂️ Catch That Crook!

**Catch That Crook!** is a 3D educational game where you play as a cashier in a toy store.  
Your goal is to catch NPCs (non-player characters) who attempt to shoplift.  
Observe their behaviour, make quick decisions, and catch them before they escape!

---

## 🎮 Game Overview
"Keep your eyes sharp. Some customers have sticky fingers."  

You are the store's only line of defence against theft. Some NPCs will attempt to steal items as they browse.  
Your job is to stop them before they leave. Each encounter requires close observation, who’s just shopping, and who’s trying to sneak out unpaid.

The game raises awareness about shoplifting and encourages ethical behaviour through fun and interactive gameplay.

---

## 🕹️ Controls

| Key | Action                        |
|-----|--------------------------------|
| **E** | Interact (Catch thief NPC)    |
| **Q** | Return to Main Menu           |

---

## 🎲 Main Gameplay
- **Observe**: Watch NPCs roam around the store and pick up items.  
- **Analyse behaviour**: Look for clues that someone might be attempting to steal (e.g. avoiding checkout, staying at a shelf for a long period).  
- **Intervene**: Confront the suspected thief before they leave.  
- **Scoring**: Gain or lose points based on correct or false accusations.  

**Rounds & Difficulty**  
- 5 NPCs spawn per round.  

---

## 🧠 NPC AI & FSM Behaviour

### 👨‍👧 Parent NPC
- Each Parent NPC has a randomized steal chance and movement speed.  
1. Patrol Shelves – follows a predefined path and may attempt to steal.  
2. **Decision: Steal or Not**  
   - If stealing: Increases speed, avoids counter, exits store.  
   - If not: Proceeds to cashier and pays.  
3. **Caught** – If player presses E nearby, parent + child disappear, score +1.  
4. **Escape** – If thief leaves undetected, score –1.  

### 🧒 Child NPC
1. Follow Parent – walks behind if parent is far.  
2. Idle – remains still if parent is close.  
3. Caught With Parent – disappears together if parent is caught.  

### 🚶 Roaming NPC
1. Walks outside the store.  
2. Non-interactive, cannot steal or be caught (background realism).  

![FSM Diagram](/Assets/Images/FSM%20Diagram.png)

---

## 📺 UI Screens
1. **Start Menu** – Play, How to Play.  
2. **In-Game UI** – Controls reminder, Score counter.  
3. **End Game Screen** – Shows Thieves Caught, Thieves Escaped, and Play Again option.  

---

## 📦 Bugs & Limitations
- Interact UI not showing.  
- Returning to menu stops NPC spawning but not active NPCs.  
- No animations to indicate stealing.  
- NPC behaviour may lead to uneventful rounds.  
- Interactions may not always register.  
- NPCs may get stuck.  
- Door does not open.
- Thief escape text not showing.

---

## 🎨 Assets & Credits

| Name             | Type         | Source                                                                 | Notes                                      |
|------------------|-------------|------------------------------------------------------------------------|--------------------------------------------|
| Store Music      | Audio       | [YouTube](https://www.youtube.com/watch?v=lWKdsawDSvU&list=RDlWKdsawDSvU) | Background music for the store             |
| Sound Effects    | Audio       | [Pixabay](https://pixabay.com/)                                        | Stealing, caught, escaped thieves          |
| Floor            | Environment | Custom                                                                 | Helps with NPC navigation                  |
| Walls            | Environment | Custom                                                                 | Exterior and interior walls of the store   |
| Roof             | Environment | Custom                                                                 | -                                          |
| Shelves          | Prop        | Custom                                                                 | -                                          |
| Toys             | Prop        | Custom                                                                 | Decorative props on shelves                |
| Door             | Prop        | Custom                                                                 | Entrance/exit                              |
| Cars             | Prop        | Custom                                                                 | Exterior visual element                     |
| Road             | Prop        | Custom                                                                 | Exterior visual element                     |
| Tunnel           | Prop        | Custom                                                                 | Exterior visual element                     |
| Exclamation Mark | VFX         | Custom                                                                 | Appears above stealing NPCs                |
| Sparkles         | VFX         | Custom                                                                 | Visual feedback for catching NPCs          |
| Particles        | VFX         | Custom                                                                 | Visual feedback for escaped thieves        |
| Start Menu       | UI          | Custom                                                                 | Buttons: Play, How to Play; game overview  |
| In-Game UI       | UI          | Custom                                                                 | Controls reminder, Score counter           |
| End Game Screen  | UI          | Custom                                                                 | Displays results + Play Again button       |

---

## ➕ Possible Additions
- Improve NPC animations and add cutscenes for immersion.  
- Improve AI with more complex behaviours.  
- Introduce different shop layouts.  
- Add difficulty levels.  

---

## ✅ Final Notes
- Built in **Unity**.  
- Designed for **laptop/desktop gameplay**.  
- Created for **educational purposes** to teach vigilance and retail ethics.  