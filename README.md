# Vampire_Survivors_Like
## Feature: ObjectPooling
### This Feature is implement the object pooling for enemy, item
---
## class
- **ObjPool**
   - **Purpose**: Object pool of one type
   - **Inheritance**: ScriptableObject, IObjectPool<GameObject>, IObservable<ObjPool>
   - **Object Pool**
      - GameObject prefab
      - Transform parent (for hierarchy)
      - int poolStartSize, poolSizeMax
      - ObjectPool<GameObject> pool
- **Stage_EnemyPool**
   - **Purpose**: Save enemy object pools step by step and initialize the next pool
   - **Inheritance**: Scriptable
   - **Function**
      - **InitializeEnemyPools**: Create an object immediately ***(Increase the index of the pool one by one for each operation.)***
      - **GetEnemyInPool**: Get object in pool
---
## Workflow
1. Create each pools using Scriptable **"ObjPool"**
2. Save ObjPool created by Scriptable **"Stage_EnemyPool"**
3. Store the **"Stage_EnemyPool"** created in Step 2 in the Stage component for creation

