import { initialize } from '@bcwdev/auth0provider-client'
import { AppState } from '../AppState'
import { audience, clientId, domain } from '../AuthConfig'
import router from '../router'
import { setBearer } from './AxiosService'
import { accountService } from './AccountService'
import { vaultsService } from './VaultsService'
import { keepsService } from './KeepsService'
// import { useRoute } from 'vue-router'

export const AuthService = initialize({
  domain,
  clientId,
  audience,
  onRedirectCallback: appState => {
    router.push(
      appState && appState.targetUrl
        ? appState.targetUrl
        : window.location.pathname
    )
  }
})

AuthService.on(AuthService.AUTH_EVENTS.AUTHENTICATED, async function() {
  // const route = useRoute()
  setBearer(AuthService.bearer)
  await accountService.getAccount()
  AppState.user = AuthService.user
  // NOTE if there is something you want to do once the user is authenticated, place that here
  vaultsService.getByAccount()
  // vaultsService.getOne(route.params.id)
  if (router.currentRoute._value.name === 'VaultPage') {
    keepsService.clearKeeps()
    vaultsService.getOne(router.currentRoute._value.params.id)
    keepsService.getByVaultId(router.currentRoute._value.params.id)
  }
})
