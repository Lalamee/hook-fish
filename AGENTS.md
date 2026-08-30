# Hook Fish — agent guidance

## Project boundary

- This is a Unity 2022.3.62f2 game. Runtime game code is under `Assets/_Sourse/`; third-party packages and asset-store content are outside the default change scope.
- Gameplay scenes are `Assets/Scenes/Bootstrap.unity`, `Menu.unity`, and `level_1.unity` through `level_20.unity`. Treat scenes and prefabs as serialized user data: preserve GUIDs and make Inspector-visible changes deliberately.
- Yandex Games (`YG`) and DOTween are runtime dependencies. Do not replace, upgrade, or remove them while addressing code-review findings.
- The working tree may contain user-prepared staged moves. Inspect `git status --short` before editing and do not stage, unstage, revert, or fold unrelated changes into the task.

## Workflow

1. Read this file, relevant code, prefab/scene references, and any applicable `CONTEXT.md` or `docs/adr/` before changing gameplay behaviour.
2. Separate changes into: mechanical C# cleanup, compatible code changes, and Unity Inspector work. Never describe a change as scene-safe without checking serialized fields, component references, and UnityEvent callbacks.
3. For a script/class or serialized-field rename, retain serialized compatibility with `FormerlySerializedAs` where appropriate; inventory and validate Inspector event bindings after the change.
4. Verify the highest available seam: compile in the target Unity version, then execute the affected player flow in Play Mode. A static-analysis pass is evidence for style only, not for gameplay or visual effects.
5. Report changed files, checks actually run, any required manual Unity steps, and residual risks. Do not commit unless the user explicitly asks.

## C# clean-code contract

- Use PascalCase for namespaces, types, interfaces (`I` prefix), public/protected/static/readonly/const members, properties, events, and methods. Use camelCase for parameters and locals. Use `_camelCase` for private/internal instance fields.
- Give every declaration an explicit access modifier. Avoid protected fields; expose behaviour through methods or properties.
- Name types for their responsibility, data members as clear nouns, and methods as accurate verbs. Split a method or type that performs two independent responsibilities rather than combining them with `And`.
- Order members: static/readonly/const fields; attributed serialized fields; remaining fields; delegates; constructors; events; properties; Unity callbacks; public/internal/private methods; event handlers. Within a group order by accessibility.
- Keep one class per file; remove unused `using` directives; sort remaining `using` directives alphabetically. Put braces on their own lines and around every conditional/loop body.
- Prefer clear expression-bodied read-only properties where they improve clarity. Avoid magic numbers: make meaningful constants or serialized configuration values.
- Events are public C# `event`s using `Action`, `Func`, or `Predicate`; name ongoing/completed events with `-ing`/`-ed`, handlers with `On`, and pair subscription/unsubscription in compatible Unity lifecycle callbacks.
- Avoid debug logging and commented-out production code. Avoid `while (true)` unless the bounded lifecycle is explicit (for example `while (enabled)` in a state-driven coroutine).

## Unity safety rules

- Do not delete an apparently unused `MonoBehaviour`, GameObject, prefab component, or script asset solely because C# search finds no reference. First search scenes and prefabs by script GUID, then state exact manual removal and Play Mode verification steps.
- Preserve serialized field names unless compatibility is added. When changing a UnityEvent-target method, list every affected scene/prefab and give a manual rebinding checklist.
- Particle, audio, animation, and completion-flow fixes require visible Play Mode proof at their trigger point and at the ordinary teardown path.
- Keep game-object wiring, component removal, visual tuning, and level-specific placement as explicit human tasks unless the user asks to edit Unity YAML and the exact target is verified.

## Agent skills

### Issue tracker

Issues and specs live in GitHub Issues for this repository. See `docs/agents/issue-tracker.md`.

### Triage labels

Use the configured canonical triage labels. See `docs/agents/triage-labels.md`.

### Domain docs

This is a single-context repository. See `docs/agents/domain.md`.
