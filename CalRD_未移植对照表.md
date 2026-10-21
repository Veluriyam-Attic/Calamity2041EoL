# 光之女皇 2.0.4.1 未移植内容 ↔ CalRD 对照表

说明：

- “2.0.4.1”列是 `CalamityMod_2041` 的类/方法/行。
- “CalRD”列是 `Calamity-Mod-Classic-1.4.5.7` 的类/方法/行。
- `CalRD 无对应` 表示 1.4.5.7 里确实没有这套系统；不是漏标。
- `CalRD 有通用钩子` 表示 CalRD 有同类基础设施，但没有光女条目，需要 addon 自己加。
- 状态里写 `addon 已实现` 的行不是“未移植”，保留是为了让你看到它对应的 CalRD 位置；真正未做的是写 `未实现`/`部分`/`缺` 的行。

---

## 1. 数值 / 全局

| # | 未移植项 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 状态 |
|---|---|---|---|---|
| 1 | 生命 100000、复仇 ×1.2、npcSlots 32 | `CalamityGlobalNPC.SetDefaults` `CalamityGlobalNPC.cs:1265-1267`、`1391-1395` | `CalamityGlobalNPC.SetDefaults` `CalamityGlobalNPC.cs:768-833`；`RevDeathStatChanges` `963`；`OtherStatChanges` `1127` | addon 已实现；CalRD 有通用钩子，无光女条目 |
| 2 | Boss 价值 60 金 | `CalamityLists` `CalamityLists.cs:2533` | `CalamityGlobalNPC.BossValues` `CalamityGlobalNPC.cs:262`；`BossValueChanges` `910` | addon 已实现；CalRD 有表，无光女条目 |
| 3 | Boss Rush 生命 200000 | `CalamityLists` `CalamityLists.cs:2498` | `CalamityGlobalNPC.BossRushHPChanges` `CalamityGlobalNPC.cs:184`；`BossRushStatChanges` `879` | addon 已实现；CalRD 有表，无光女条目 |
| 4 | 专家接触倍率 0.6875 | `NPCStats` `NPCStats.cs:228` | `NPCStats.ExpertDamageMultiplier` `NPCStats.cs:47`，表在 `144` | addon 已实现；CalRD 有表，无光女条目 |
| 5 | 接触伤害表 80/110/143/176/231 | `NPCStats` `NPCStats.cs:444-448` | `NPCStats.ContactDamageValues` `NPCStats.cs:48`，表在 `262`；应用在 `CalamityGlobalNPC.OtherStatChanges` `CalamityGlobalNPC.cs:1238` | addon 已实现；CalRD 有表，无光女条目 |
| 6 | 弹幕伤害表 4 条 | `NPCStats` `NPCStats.cs:688-691` | `NPCStats.ProjectileDamageValues` `NPCStats.cs:49`，表在 `517`；`GetProjectileDamage` `77` | addon 只加了彩虹一条；日舞/持久彩虹/光矛三条未加 |
| 7 | 基础 DR 0.15、击杀时间 10800 | `CalamityMod` `CalamityMod.cs:624`、`693` | `CalamityMod.DRValues` `CalamityMod.cs:365`；`bossKillTimes` `483` | addon 已实现；CalRD 有表，无光女条目 |
| 8 | 白天动态 DR | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:3238-3259` | `CalamityGlobalNPC.ApplyDR` `CalamityGlobalNPC.cs:1479-1528`；`ModifyIncomingHit` `1399` | addon 已写 `ModifyIncomingHit`，但只算 `effectiveDR` 没有写回 `modifiers`，等于未生效 |
| 9 | 接触破防 10 秒 | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:2309-2312` | CalRD 没有 `canBreakPlayerDefense`/`DealsDefenseDamage`；最近的是 `CalamityPlayer.ModifyHitByProjectile` `CalamityPlayer.cs:6047` | CalRD 无对应；需要 addon 自己给玩家加 `defenseDamage` 类效果 |
| 10 | 白天动态 DR 关闭条件（Boss Rush/Cirrus） | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:3238-3259` | `CalamityGlobalNPC.ApplyDR` `1479-1528` 里只有 `!BossRushActive`，没有 Cirrus 判断 | CalRD 有通用判断；光女专属条件未实现 |
| 11 | bossTypes 编号 43 | `CalamityLists` `CalamityLists.cs:2593` | CalRD 没有 `bossTypes` 表；最近的是 `CalamityGlobalNPC.GetDownedBossVariable` `CalamityGlobalNPC.cs:4473` | CalRD 无对应 |
| 12 | 光女 downed 状态 | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:7654-7655` | `CalamityGlobalNPC.GetDownedBossVariable` `CalamityGlobalNPC.cs:4473`；`World/CalamityWorld.cs` 的 downed 字段 | CalRD 有通用钩子，无光女分支 |
| 13 | Boss head 图标 | `CalamityGlobalNPC.BossHeadSlot` `CalamityGlobalNPC.cs:3342` | `CalamityGlobalNPC.BossHeadSlot` `CalamityGlobalNPC.cs:1603` | CalRD 有钩子，无光女条目 |
| 14 | 速通分段 / 图标 | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:7784-7803`；`CalamityGlobalNPCLoot` `1886-1888`；`SpeedrunTimerUI` `138` | CalRD 没有 `SpeedrunTimerUI`/`lastSplitType`；没有对应类 | CalRD 无对应 |

---

## 2. AI

| # | 未移植项 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 状态 |
|---|---|---|---|---|
| 15 | 复仇/Boss Rush 的 Buffed AI | `EmpressofLightAI.BuffedEmpressofLightAI` `EmpressofLightAI.cs:15-1278` | `CalamityGlobalNPC.PreAI` `CalamityGlobalNPC.cs:1623`；Buffed 分发表在 `1664-1787`，没有 `HallowBoss` case | CalRD 有 Boss AI 分发表，无光女 AI |
| 16 | 非复仇 Vanilla AI | `EmpressofLightAI.VanillaEmpressofLightAI` `EmpressofLightAI.cs:1280-2326` | CalRD 不重写光女，走原版 `PreAI`；无对应方法 | CalRD 无对应 |
| 17 | AI 分发：复仇/Boss Rush → Buffed | `CalamityGlobalNPC.PreAI` `CalamityGlobalNPC.cs:3486` + `3555-3556` | `CalamityGlobalNPC.PreAI` `CalamityGlobalNPC.cs:1664` 的 `if (CalamityWorld.revenge || BossRushActive)` | CalRD 有同样分发结构，无光女 case |
| 18 | AI 分发：非复仇 → Vanilla | `CalamityGlobalNPC.PreAI` `CalamityGlobalNPC.cs:3688-3689` | CalRD `PreAI` 的默认路径 | CalRD 无光女专属分支 |
| 19 | `CreateSpawnDust` | `EmpressofLightAI.CreateSpawnDust` `EmpressofLightAI.cs:2328-2352` | `CalamityGlobalNPC.SpawnDustOnNPC` `CalamityGlobalNPC.cs:5290`（通用生成 dust） | CalRD 有通用 dust 工具，无光女专用 |
| 20 | `AI_120_HallowBoss_DoMagicEffect` | `EmpressofLightAI` `EmpressofLightAI.cs:2354-2443` | CalRD `CalamityGlobalAI` `CalamityGlobalAI.cs:22` 里没有对应方法 | CalRD 无对应 |
| 21 | case 0-13 全部攻击 | `EmpressofLightAI` `EmpressofLightAI.cs:107-1257` | CalRD `CalamityGlobalNPC.PreAI` `1664-1787` 的 switch 是插入点；没有光女 case | CalRD 无光女 case |
| 22 | case 11/12（前方光矛/原地追踪彩虹） | `EmpressofLightAI` `EmpressofLightAI.cs:998-1173` | 同上，无光女 case | CalRD 无对应 |
| 23 | phase2Attack7/num54 重复索引 | `EmpressofLightAI` `EmpressofLightAI.cs:302/318`、`1540/1543` | CalRD 没有这两套 AI 方法 | CalRD 无对应 |
| 24 | CurrentlyEnraged / CurrentlyIncreasingDefenseOrDR | `EmpressofLightAI` `EmpressofLightAI.cs:48`、`42`、`1315`、`1309` | CalRD 没有这两个字段（搜不到）；`CalamityGlobalNPC` 也没有 | CalRD 完全无对应 |
| 25 | Boss Rush 强制狂暴 IL | `ILEditing/MechanicILChanges` `MechanicILChanges.cs:291-298` | `ILEditing/ILChanges.Load` `ILChanges.cs:40`；没有 `ShouldEmpressBeEnraged` detour | CalRD 有 IL 加载器，无光女 hook |
| 26 | Boss Rush 夜战上下文 | `Events/BossRushEvent` `BossRushEvent.cs:249` | `Events/BossRushEvent` 的 `Boss` 列表 `BossRushEvent.cs:85-190`；没有 `HallowBoss` | CalRD 有 Boss Rush 列表，无光女条目 |
| 27 | Boss Rush 200000 与 master/death 参数 | `CalamityLists` `CalamityLists.cs:2498`；`EmpressofLightAI` `Buffed` 15-1278 | `CalamityGlobalNPC.BossRushStatChanges` `CalamityGlobalNPC.cs:879`；`BossRushHPChanges` `184` | addon 只改了 HP，没有 master/death/夜战行为 |
| 28 | 多玩家补弹 / 脱战 / 阶段时间 | `EmpressofLightAI` `Buffed` 15-1278；`Vanilla` 1280-2326 | CalRD 没有光女 AI；`CalamityGlobalNPC.PreAI` 只是分发 | CalRD 无对应 |

---

## 3. 弹幕

| # | 未移植项 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 状态 |
|---|---|---|---|---|
| 29 | 彩虹弹 140 帧无伤 + 追踪 | `CalamityGlobalProjectile` `CalamityGlobalProjectile.cs:1177-1233`、`4318-4327` | `CalamityGlobalProjectile.PreAI` `CalamityGlobalProjectile.cs:133`；`AI` `793`；`CanDamage` `1967`；`CanHitPlayer` `2208` | CalRD 有通用弹幕钩子，无光女弹幕条目 |
| 30 | 持久彩虹 60 帧无伤 + 加速 + 拖尾衰减 | `CalamityGlobalProjectile` `CalamityGlobalProjectile.cs:3509-3514`、`4318-4327`；`CalamityPlayerHitHurt` `992-1015` | `CalamityGlobalProjectile.AI` `793`；`CalamityPlayer.ModifyHitByProjectile` `CalamityPlayer.cs:6047` | CalRD 有通用钩子，无光女弹幕条目 |
| 31 | 死亡极光纯视觉 | `EmpressofLightAI` case 0 `107-146`；`DrawingUtils` `27-28` | `CalamityGlobalProjectile.PreDraw` `CalamityGlobalProjectile.cs:2024`；无光女分支 | CalRD 无对应 |
| 32 | 光女弹幕常量（伤害/速度等） | `CalamityGlobalProjectile` `CalamityGlobalProjectile.cs:127-130` | `CalamityGlobalProjectile.SetDefaults` `CalamityGlobalProjectile.cs:82`；无光女分支 | CalRD 有通用钩子，无光女条目 |
| 33 | 日舞/持久彩虹/光矛 3 条伤害表 | `NPCStats` `NPCStats.cs:688-691` | `NPCStats.ProjectileDamageValues` `NPCStats.cs:517` | addon 只加了彩虹，缺 3 条 |
| 34 | 光矛/日舞/持久彩虹破防 | `CalamityGlobalProjectileTweaks` `305-309` | CalRD 没有 `canBreakPlayerDefense`；`CalamityPlayer.ModifyHitByProjectile` `CalamityPlayer.cs:6047` 只是改伤害 | CalRD 无对应 |
| 35 | SplitShotCore 只挂 debuff、光女 AI 不生成 | `CalamityPlayerHitHurt` `1365-1375` | `CalamityPlayer.ModifyHitByProjectile` `CalamityPlayer.cs:6047`；没有全局 `GlobalProjectile.OnHitPlayer` | CalRD 无直接对应；addon 目前只挂了 60 帧 debuff |

---

## 4. Debuff / 弱点

| # | 未移植项 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 状态 |
|---|---|---|---|---|
| 36 | 弱点标签 Cold/Heat/Sickness=true，Water/Electricity=null | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:1791` + `2041-2043` | CalRD 没有 `VulnerableToCold/Heat/Sickness/Water/Electricity` 字段；搜不到 | CalRD 完全无对应 |
| 37 | 弱点 DoT 倍率（×2 / ×0.5 / ×1） | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:790-842` | CalRD `UpdateLifeRegen` `CalamityGlobalNPC.cs:498`；固定 DoT `729-745`；`ApplyDPSDebuff` `748` | CalRD 只有固定 DoT，无弱点倍率系统 |
| 38 | 冷弱点强化 Glacial State / Temporal Sadness | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:757-768` | CalRD `UpdateLifeRegen` 的减速逻辑在 `UpdateLifeRegen` 前段/`GlacialState` Buff；没有光女弱点加成 | CalRD 有 Debuff 本体，无弱点加成 |
| 39 | 接触 Debuff：白天圣火/夜晚夜凋零 240 帧 | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:5324-5326` | `CalamityGlobalNPC.OnHitPlayer` `CalamityGlobalNPC.cs:2959` | addon 已实现；CalRD 有通用 `OnHitPlayer` |
| 40 | 弹幕 Debuff：光矛/彩虹 60 帧、持久彩虹 120 帧、日舞 180 帧 | `CalamityPlayerHitHurt` `CalamityPlayerHitHurt.cs:1365-1375` | `CalamityPlayer.ModifyHitByProjectile` `CalamityPlayer.cs:6047`；具体弹幕类各自 `OnHitPlayer` | addon 只做了 60 帧部分，120/180 未做 |
| 41 | Debuff 免疫 On Fire/On Fire3/HolyFlames/Nightwither | `NPCDebuffs` `NPCDebuffs.cs:207` | `CalamityGlobalNPC.DebuffImmunities` `CalamityGlobalNPC.cs:837` | addon 已实现；CalRD 有通用免疫钩子 |
| 42 | Boss 默认免疫 Pearl Aura | `CalamityLists` `CalamityLists.cs:934`；`NPCDebuffs` `99` | `CalamityLists.enemyImmunityList` `CalamityLists.cs:466`；`DebuffImmunities` `CalamityGlobalNPC.cs:839-846` | CalRD 通用 Boss 已实现，无需光女专属 |
| 43 | HolyFlames/Nightwither 定义与 LongerExpertDebuff | `HolyFlames.cs:10-18`、`Nightwither.cs:10-18`、`CalamityPlayerLifeRegen.cs:104-105` | `Buffs/DamageOverTime/HolyFlames.cs:7`、`Nightwither.cs:7`；DoT 在 `CalamityGlobalNPC.UpdateLifeRegen` `729-745` | CalRD 有 Debuff 定义，无 `LongerExpertDebuff` 光女专属逻辑 |
| 44 | Debuff 死亡提示/绘制 | `CalamityPlayerHitHurt` `423-426`、`463-466`；`CalamityPlayerDrawEffects` `251-267` | `CalamityPlayer` `3969`、`4010`；`CalamityPlayerDrawEffects` `405`、`458` | CalRD 有通用 HUD/死亡提示，无光女专属文本 |

---

## 5. 掉落 / 物品

| # | 未移植项 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 状态 |
|---|---|---|---|---|
| 45 | 本体掉落树重写（7 件 1/4、翱翔徽章、画、复仇、GFB、Lore） | `CalamityGlobalNPCLoot` `CalamityGlobalNPCLoot.cs:1522-1574` | `CalamityGlobalNPCLoot.PreKillModifyNPCLoot` `CalamityGlobalNPCLoot.cs:280`；`DropHelper.CalamityStyle` `DropHelper.cs:935` | addon 已实现本体 7 件；Lore/画/GFB 未做 |
| 46 | 女皇宝藏袋（7 件 1/3、画、复仇饰品） | `CalamityGlobalItemLoot` `CalamityGlobalItemLoot.cs:231-246`；删除规则 `484-514` | `CalamityGlobalItem.ModifyItemLoot` `CalamityGlobalItem.cs:2888`；`DropHelper.AddRevBagAccessories` `DropHelper.cs:1217` | addon 已实现袋子 7 件；画未做 |
| 47 | Lore 物品 + FairyQueenTrophy/Bookcase 配方 | `LoreEmpressofLight` `LoreEmpressofLight.cs:6-27` | `Items/LoreItems/LoreItem` 基类 `LoreItem.cs:6`；CalRD 没有光女 Lore | CalRD 有 Lore 基类，无光女 Lore |
| 48 | ThankYouPainting 1/100 | `ThankYouPainting` `ThankYouPainting.cs:11-31` | CalRD 没有 `DevPaintings` 目录/对应物品 | CalRD 无对应 |
| 49 | GFB FabsolsVodka + Clentaminator2 | `CalamityGlobalNPCLoot` `CalamityGlobalNPCLoot.cs:1568-1569` | `FabsolsVodka` 物品 `FabsolsVodka.cs:9`；`Clentaminator2` 在 CalRD 搜不到；掉落钩子 `CalamityGlobalNPCLoot.cs:280` | CalRD 有 FabsolsVodka，GFB 掉落/Clentaminator2 未实现 |
| 50 | 女皇之翼/翱翔徽章/武器数值 | `CalamityGlobalItem` `CalamityGlobalItem.cs:137-138`、`167-169`、`189`；`CalamityGlobalItemTweaks` `99/103/424/443/496` | `CalamityGlobalItem.SetDefaults` `CalamityGlobalItem.cs:114`；`ModifyWeaponDamage` `1015`；`UpdateAccessory` `3170`；`HorizontalWingSpeeds` `3504`；`ModifyTooltips` `1043` | CalRD 有通用物品钩子，无光女条目 |
| 51 | 元素斧/元素短剑/熔脸器/晋升徽章配方 | `ElementalAxe.cs:47-57`、`ElementalShiv.cs:35-44`、`FaceMelter.cs:84-93`、`AscendantInsignia.cs:35-44` | `ElementalAxe.AddRecipes` `ElementalAxe.cs:53-61`；`FaceMelter.AddRecipes` `FaceMelter.cs:48-56`；`ElementalShiv`/`AscendantInsignia` 搜不到 | CalRD 有元素斧/熔脸器旧配方，无短剑/晋升徽章 |
| 52 | 女皇之翼进 AnyWings | `CalamityRecipes` `CalamityRecipes.cs:504` | CalRD 没有 `AnyWings`；`CalamityRecipes` 类 `CalamityRecipes.cs:20` | CalRD 无对应 |
| 53 | 万花筒 tag -20/+12% | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:5644-5646`；`BalancingConstants` `84` | CalRD 没有 `KaleidoscopeTagDamageMultiplier`/`RainbowWhipNPCDebuff` | CalRD 无对应 |
| 54 | 星光穿透例外 | `CalamityLists` `CalamityLists.cs:1911` | CalRD 没有 `pierceResistExceptionList` | CalRD 无对应 |
| 55 | 月光石/太阳石/天界石/天界壳降低圣火/夜凋零 | `CalamityGlobalItem` `1188-1196`；`Tooltip` `600-619` | CalRD 只有原版 `ItemID.MoonStone` 等，无光女 DoT 减免 | CalRD 无对应 |
| 56 | 狂信徒对夜光弹幕 20% 抗性 | `BalancingChangesManager` `242` | CalRD 没有 Nightglow 弹幕/对应抗性 | CalRD 无对应 |
| 57 | GFB 刃边铁路弓包含暮光弹幕 | `BladedgeRailbow` `37` | CalRD 搜不到 `BladedgeRailbow` | CalRD 无对应 |
| 58 | 亵渎水晶白天/夜晚免疫圣火/夜凋零 | `CalamityPlayerMiscEffects` `3306-3307`；`ProfanedCrystalWhipDebuff` `21-35` | CalRD 搜不到 `ProfanedCrystalWhip` | CalRD 无对应 |

---

## 6. 相关系统

| # | 未移植项 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 状态 |
|---|---|---|---|---|
| 59 | 七彩蝴蝶刷新（黄昏到午夜、10%） | `CalamityGlobalNPC` `CalamityGlobalNPC.cs:6091-6096` | `CalamityGlobalNPC.EditSpawnPool` `CalamityGlobalNPC.cs:3617`；无光女蝴蝶 | CalRD 有刷怪钩子，无光女蝴蝶 |
| 60 | FAP 售卖七彩蝴蝶 | `FAP` `FAP.cs:905` | `FAP.AddShops` `FAP.cs:255`；没有 `Prismatic Lacewing` | CalRD 有商店，无该商品 |
| 61 | Boss Checklist 收藏品 + 15.0 注释 | `WeakReferenceSupport` `1060`、`143` | `WeakReferenceSupport.BossChecklistSupport` `WeakReferenceSupport.cs:165`；`AddBoss` `133` | CalRD 有 Boss Checklist 支持，无光女条目 |
| 62 | SCal Cirrus 20% 血召唤 1 Providence + 2 光女 + 2 史莱姆皇后 | `SupremeCalamitas` `SupremeCalamitas.cs:1952`、`1963-1987` | `SupremeCalamitas.secondStage` `SupremeCalamitas.cs:51`；CalRD 没有 Cirrus/光女召唤 | CalRD 有 SCal，无 Cirrus 阶段 |
| 63 | The Community 计数 42 Boss | `TheCommunity` `44`、`73` | `CalamityPlayer.community` `CalamityPlayer.cs:1238`；`TheCommunity.UpdateAccessory` `TheCommunity.cs:41` | CalRD 有 Community，无光女计数 |
| 64 | Lore 文本 / DownedEoL 文本 | `Localization/...Lore.hjson:317-328`；`Condition.hjson:33` | CalRD 没有光女 Lore/Downed 文本 | CalRD 无对应 |
| 65 | `PostEoL` 条件 API | `DropHelper` `DropHelper.cs:602` | CalRD 没有 `PostEoL` | CalRD 无对应 |

---

## 7. 已移植/已由 CalRD 通用实现（避免重复找）

| # | 内容 | 2.0.4.1 类 / 行 | CalRD 对应类 / 行 | 当前 addon |
|---|---|---|---|---|
| A | 生命/复仇/slots | `CalamityGlobalNPC.SetDefaults` `1265-1267`、`1391-1395` | `CalamityGlobalNPC.SetDefaults` `768`；`RevDeathStatChanges` `963` | 已实现 |
| B | Boss 价值/Boss Rush HP | `CalamityLists` `2533`、`2498` | `BossValues` `262`；`BossRushHPChanges` `184` | 已实现 |
| C | 专家倍率/接触表 | `NPCStats` `228`、`444-448` | `NPCStats` `144`、`262` | 已实现 |
| D | 基础 DR/击杀时间 | `CalamityMod` `624`、`693` | `DRValues` `365`；`bossKillTimes` `483` | 已实现 |
| E | 本体/袋子 7 件 CalamityStyle | `CalamityGlobalNPCLoot` `1522-1574`；`CalamityGlobalItemLoot` `231-246` | `PreKillModifyNPCLoot` `280`；`ModifyItemLoot` `2888`；`DropHelper.CalamityStyle` `935` | 已实现 |
| F | 接触 Debuff | `CalamityGlobalNPC` `5324-5326` | `OnHitPlayer` `2959` | 已实现 |
| G | Debuff 免疫 | `NPCDebuffs` `207` | `DebuffImmunities` `837` | 已实现 |
| H | Pearl Aura 免疫 | `CalamityLists` `934` | `enemyImmunityList` `466`；`DebuffImmunities` `839-846` | CalRD 通用已实现 |
