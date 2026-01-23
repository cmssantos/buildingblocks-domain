#!/usr/bin/env bash
set -e

BASE_VERSION=$(grep '<Version>' src/*/*.csproj | sed -E 's/.*>(.*)<.*/\1/')
BRANCH=$(git rev-parse --abbrev-ref HEAD)
COMMIT=$(git rev-parse --short HEAD)

case "$BRANCH" in
  main)
    VERSION=$BASE_VERSION
    ;;
  develop)
    VERSION="$BASE_VERSION-alpha.$(date +%Y%m%d)"
    ;;
  feature/*)
    NAME=${BRANCH#feature/}
    VERSION="$BASE_VERSION-feature.$NAME.$COMMIT"
    ;;
  hotfix/*)
    VERSION="$BASE_VERSION-hotfix.$COMMIT"
    ;;
  *)
    VERSION="$BASE_VERSION-dev.$COMMIT"
    ;;
esac

echo "$VERSION"
