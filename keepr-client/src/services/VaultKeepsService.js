import { AppState } from '../AppState'
import { api } from './AxiosService'
import { keepsService } from './KeepsService'

class VaultKeepsService {
  async create(body) {
    await api.post('api/vaultkeeps', body)
    AppState.activeKeep.keeps += 1
  }

  async delete(id, vaultId) {
    await api.delete('api/vaultkeeps/' + id)
    keepsService.getByVaultId(vaultId)
  }
}
export const vaultKeepsService = new VaultKeepsService()
