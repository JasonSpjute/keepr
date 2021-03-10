import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async createKeep(body) {
    const res = await api.post('api/keeps', body)
    AppState.keeps.push(res.data)
  }

  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getOne(id) {
    const res = await api.get('api/keeps/' + id)
    AppState.activeKeep = res.data
  }

  async getByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.keeps = res.data
  }

  async getByVaultId(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    AppState.keeps = res.data
  }

  async delete(id) {
    await api.delete('api/keeps/' + id)
    // this.getKeeps()
    const keeps = AppState.keeps
    const removeIndex = keeps.map(function(item) { return item.id }).indexOf(id)
    keeps.splice(removeIndex, 1)
    AppState.keeps = keeps
  }

  clearKeeps() {
    AppState.keeps = {}
  }
}

export const keepsService = new KeepsService()
