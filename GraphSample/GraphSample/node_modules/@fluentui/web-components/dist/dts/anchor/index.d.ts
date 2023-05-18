import { Anchor } from '@microsoft/fast-foundation';
import { ButtonAppearance } from '../button';
/**
 * Types of anchor appearance.
 * @public
 */
export declare type AnchorAppearance = ButtonAppearance | 'hypertext';
/**
 * The Fluent Anchor Element. Implements {@link @microsoft/fast-foundation#Anchor},
 * {@link @microsoft/fast-foundation#AnchorTemplate}
 *
 *
 * @public
 * @remarks
 * HTML Element: \<fluent-anchor\>
 *
 * {@link https://developer.mozilla.org/en-US/docs/Web/API/ShadowRoot/delegatesFocus | delegatesFocus}
 */
export declare class FluentAnchor extends Anchor {
    /**
     * The appearance the anchor should have.
     *
     * @public
     * @remarks
     * HTML Attribute: appearance
     */
    appearance: AnchorAppearance;
    appearanceChanged(oldValue: AnchorAppearance, newValue: AnchorAppearance): void;
    /**
     * @internal
     */
    connectedCallback(): void;
    /**
     * Applies 'icon-only' class when there is only an SVG in the default slot
     *
     * @internal
     */
    defaultSlottedContentChanged(): void;
}
/**
 * Styles for Anchor
 * @public
 */
export declare const AnchorStyles: import("@microsoft/fast-element").ElementStyles;
