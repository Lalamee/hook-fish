# Issue tracker: GitHub

Issues and specs for this repository live as GitHub Issues. Use GitHub CLI (`gh`) from the repository root for reads and writes.

## Conventions

- Create an issue with `gh issue create`; use a multiline body.
- Read an issue and its discussion with `gh issue view <number> --comments`.
- Apply the configured triage label when publishing a ready issue.
- Infer the repository from the `origin` remote.
- Pull requests are not a triage surface.

## When a skill says "publish to the issue tracker"

Create a GitHub Issue. If GitHub authentication is unavailable, prepare the English issue body locally for review and state that publication remains pending; do not substitute a different tracker without user approval.
