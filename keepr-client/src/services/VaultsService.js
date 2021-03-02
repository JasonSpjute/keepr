import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getByAccountId() {
    const res = await api.get('api/account/vaults')
    AppState.vaults = res.data
  }
}

export const vaultsService = new VaultsService()
