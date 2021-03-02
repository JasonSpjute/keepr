import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getByAccount() {
    const res = await api.get('api/account/vaults')
    AppState.myVaults = res.data
  }

  async getByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
  }
}

export const vaultsService = new VaultsService()
