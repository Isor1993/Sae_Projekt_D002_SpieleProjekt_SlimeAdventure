/*****************************************************************************
* Project : Spielprojekt (K1, S1, S2, S3)
* File    : IDamageable.cs
* Date    : 20.02.2026
* Author  : Eric Rosenberg
*
* Description :
* Defines a contract for objects that can receive damage.
* Any class implementing this interface must provide
* logic for handling incoming damage values.
*
* History :
* 20.02.2026 ER Created
******************************************************************************/

public interface IDamageable
{
    /// <summary>
    /// Applies damage to the implementing object.
    /// The concrete class defines how health reduction,
    /// death behavior, or additional effects are handled.
    /// </summary>
    /// <param name="damage">
    /// The amount of damage to apply.
    /// </param>
    public void TakeDamge(int damage);
}
