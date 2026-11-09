# Maçonaria

Trabalho de Desenvolvimento de Jogos Digitais — Miguel Maldonado Ferreira e Daniel Arlindo.

Jogo de sobrevivência em uma arena 3D, feito em Unity e C#. O personagem é um penitente preso no pátio de uma catedral em ruínas. O objetivo é resistir por três minutos aos inimigos que aparecem pelo cenário.

O ataque é automático. O jogador precisa se movimentar para evitar os inimigos e recolher os fragmentos deixados por eles. Cada inimigo derrotado vale 10 pontos. A cada 5 fragmentos, o personagem sobe de nível e passa a disparar mais rápido.

## Controles

- **WASD ou setas:** movimentar.
- **R:** reiniciar depois de vencer ou perder.

## Como abrir

Use o **Unity 2022.3.62f3 LTS**.

1. Baixe e extraia o projeto.
2. No Unity Hub, clique em **Add → Add project from disk** e selecione a pasta que contém `Assets`, `Packages` e `ProjectSettings`.
3. Aguarde a importação. A cena e os prefabs são criados na primeira abertura.
4. Se a cena não abrir, vá em **Maçonaria → Preparar cena inicial**.
5. Clique em **Play**.

## Andamento

A primeira versão usa modelos simples, uma arena, uma arma e um tipo de inimigo. Ainda falta validar a compilação e testar a partida completa no computador da apresentação.

[Escopo do trabalho](docs/escopo.md) · [Testes pendentes](docs/validacao.md)
