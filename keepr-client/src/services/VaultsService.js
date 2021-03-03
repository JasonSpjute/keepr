import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async createVault(body) {
    const res = await api.post('api/vaults', body)
    AppState.myVaults.push(res.data)
  }

  async getOne(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.currentVault = res.data
  }

  async getByAccount() {
    const res = await api.get('api/account/vaults')
    AppState.myVaults = res.data
  }

  async getByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
  }

  async edit(body, id) {
    await api.put('api/vaults/' + id, body)
    this.getOne(id)
    this.getByAccount()
  }

  async delete(id) {
    await api.delete('api/vaults/' + id)
    this.getByAccount()
  }
}

export const vaultsService = new VaultsService()
