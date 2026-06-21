# Gerenciador de Chamados

**Aluno:** Manoel Rodrigues  
**Disciplina:** Programação Orientada a Objetos I — FSG  
**Entrega:** Entrega Final 26/06/2026

## Como executar

```bash
dotnet run
```

## Classes

- `Chamado` — entidade principal com ID, descrição, status e datas. Aplica as regras de negócio: não permite concluir um chamado já concluído e registra a data de conclusão automaticamente.
- `Gerenciador` — orquestra a lista de chamados em memória. Responsável por criar, listar, buscar e concluir chamados.
- `RepositorioChamados` — lê e grava o arquivo `chamados.json`. Não contém regras de negócio.
- `StatusChamado` — enum com os valores `Aberto` e `Concluido`.
- `MenuConsole` — interface de console com 4 telas: menu principal, novo chamado, consulta e detalhe.

## Diagrama de Classes 

<img width="1280" height="768" alt="image" src="https://github.com/user-attachments/assets/25e454fa-804b-4a83-a1fa-4d5d6035de11" />


## Persistência

Os dados são salvos automaticamente em `chamados.json` na pasta do executável após cada operação de criação ou conclusão de chamado, e também ao sair do programa.

## Telas 
### Tela 1 - Menu 
<img width="395" height="297" alt="Captura de Tela 2026-06-21 às 13 01 54" src="https://github.com/user-attachments/assets/4ca088af-500d-4e07-9643-f1f9d12cb610" />

### Tela 2 - Abertura de chamado
<img width="400" height="351" alt="Captura de Tela 2026-06-21 às 13 03 15" src="https://github.com/user-attachments/assets/684e4a4b-cb47-4022-9aa8-d762ed6ed237" />

### Tela 3 - Consulta de chamado
<img width="539" height="296" alt="Captura de Tela 2026-06-21 às 13 03 30" src="https://github.com/user-attachments/assets/42b28a63-45ed-4fb4-88af-59c5b0d6831e" />

### Tela 4 - Detalhes do chamado
<img width="398" height="411" alt="Captura de Tela 2026-06-21 às 13 03 39" src="https://github.com/user-attachments/assets/9b789c60-c242-437f-880f-513c70a21440" />
