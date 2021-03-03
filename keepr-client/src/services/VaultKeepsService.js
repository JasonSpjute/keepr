import { api } from './AxiosService'

class VaultKeepsService {
  async create(body) {
    await api.post('api/vaultkeeps', body)
  }
}
export const vaultKeepsService = new VaultKeepsService()
