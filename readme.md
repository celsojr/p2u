## p2u (Paste to unix)

Paste text contents with unix like line endings into Windows Terminal without line breakings

### Installation

```dotnet tool install --global p2u --version 1.0.0```

After installation, copy any bash multi line command from the internet

e.g.
```
helm install --name redis-cluster \
  --set cluster.slaveCount=3 \
  --set password=password \
  --set securityContext.enabled=true \
  --set securityContext.fsGroup=2000 \
  --set securityContext.runAsUser=1000 \
  --set volumePermissions.enabled=true \
  --set master.persistence.enabled=true \
  --set slave.persistence.enabled=true \
  --set master.persistence.enabled=true \
  --set master.persistence.path=/data \
  --set master.persistence.size=8Gi \
  --set master.persistence.storageClass=manual \
  --set slave.persistence.enabled=true \
  --set slave.persistence.path=/data \
  --set slave.persistence.size=8Gi \
  --set slave.persistence.storageClass=manual \
stable/redis
```

convert line endings by doing just

```p2u -w```

and paste into Windows terminal like you do in other unix system terminals