# Vampire_Survivors_Like
## Todo: Item
### 1. Item Type
#### Weapons
- Player Base Attack: Take one from Mille or Shooting type
- Close Range: Area Scan
  - Draw circle
  - Rotate around the starting point while gradually moving away
- Shooting: throwing some projectile
- Single Shoot, Penetrate, High Damage
- Multiple Shoot, Penetrate, Low Damage
- Boomerang
  - Beam
- Summon
  - Move To Enemy and Crash Attack
  - Move around the player and attack the enemy
#### Support
- Healing
- Shield
- Gain exp
#### Drop
- Exp
- Heal
- Bonus reward chance
---
### 2. Item Level
- Max Level: 7
- Min Level: 1
- Level up when selecting a reward that matches an item already owned
---
### 3. Item Slot
- Attack: 5(include base attack item)
- Support: 5
---
### 4. The probability of an item appearing
- Attack, Support: Higher chance for items already owned to appear
- Drop: Enemy is Dead
  - Exp: 100%
  - Heal: 5%
  - Bonus reward chance: When the boss is dead
---
## Item Management
### Drop Item
- Exp
  - Life Time: 3 minute
  - Drop probability: 100%
  - Use Object Poolling
    - min: 100(Generated at startup)
    - max: 300
- Heal
  - Life Time: 3 minute
  - Drop probability: 10%
- magnet
  - Life Time: infinity
  - Drop probability: 3%
- Reward
  - Lift Time: infinity
  - Drop probability:
    - Boss: 100%
    - other: 1%(or counting kill: 200)
