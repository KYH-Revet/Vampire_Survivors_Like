# Vampire_Survivors_Like
## Feature: ObjectPooling
### This Feature is implement the object pooling for enemy, item
---
## interface
- **IObjPooling**
   - **Purpose**: 
   - **Functions**
      1. **InitPool**: Create an object immediately
      2. **GetPooledObject**: Get object from pool
      3. **ReturnToPool**: Object return to pool
      4. **ClearPool**: Destroy object in pool
      5. **SlowInitPool**: Create an object gradually
---
## class
- **ObjPool**
   - **Purpose**: One object pool
   - **Inheritance**: Scriptable, IObjPooling
   - **Object Pool**
      - GameObject prefab
      - int startSize, sizeMax
      - Queue<GameObject> pool
   - **Coroutine Function**
      - **IEnumerator SlowInitCoroutine**: Create an object gradually(For SlowInitPool)
- **Stage_EnemyPool**
   - **Purpose**: Save enemy object pools step by step and initialize the next pool
   - **Inheritance**: Scriptable
   - **Function**
      - ***The following functions increase the index of the pool one by one for each operation.***
      - **InitializeEnemyPools**: Create an object immediately
      - **SlowInitializeEnemyPools**: Create an object gradually
---
## Workflow
1. Create each pools using Scriptable **"ObjPool"**
2. Save ObjPool created by Scriptable **"Stage_EnemyPool"**
3. Store the **"Stage_EnemyPool"** created in Step 2 in the Stage component for creation
