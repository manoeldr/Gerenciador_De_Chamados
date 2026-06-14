# Gerenciador de Chamados

**Aluno:** Manoel Rodrigues  
**Disciplina:** Programação Orientada a Objetos I — FSG  
**Entrega:** 1ª entrega (15/06/2026)

## Como executar

```bash
dotnet run
```

## Classes

- `Chamado` — entidade principal com ID, descrição, status e datas. Contém as regras de negócio: não permite concluir um chamado já concluído e registra a data de conclusão automaticamente.
- `Gerenciador` — orquestra a lista de chamados em memória. Responsável por criar, listar, buscar e concluir chamados.
- `RepositorioChamados` — lê e grava o arquivo `chamados.json`. Não contém regras de negócio.
- `StatusChamado` — enum com os valores `Aberto` e `Concluido`.

## Diagrama de Classes 

<img width="1280" height="768" alt="image" src="https://github.com/user-attachments/assets/25e454fa-804b-4a83-a1fa-4d5d6035de11" />


## Persistência

Os dados são salvos automaticamente em `chamados.json` na pasta do executável após cada operação de criação ou conclusão.
